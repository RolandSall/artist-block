using System.Security.Claims;
using System.Text.Json.Serialization;
using account_service.Repository;
using account_service.Repository.CollectionRepo;
using account_service.Repository.GanRepo;
using account_service.Repository.BuyRepo;

using account_service.Repository.PaintingRepo;
using account_service.Repository.RegistrationRepo;
using account_service.Repository.ReviewRepo;
using account_service.Repository.SearchRepo;
using account_service.Repository.SpecialityRepo;
using account_service.Service.CollectionService;
using account_service.Service.BuyController;
using account_service.Service.CurrentLoggedInService;
using account_service.Service.GanService;
using account_service.Service.PaintingService;
using account_service.Service.RegistrationService;
using account_service.Service.ReviewService;
using account_service.Service.SearchService;
using account_service.Service.SpecialityService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace account_service{
    public class Startup {
             private readonly string _CORSPolicy = "_CORSPolicy";
        private readonly IWebHostEnvironment _env;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services) {
            // services.AddControllers();
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles) // to make enum values appear as string in swagger
                .AddJsonOptions( x=> x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            
            // Injections
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IRegistrationRepo, RegistrationRepo>();

            services.AddScoped<IPaintingService, PaintingService>();
            services.AddScoped<IPaintingRepo , PaintingRepo>();

            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewRepo, ReviewRepo>();
            
            services.AddScoped<ISpecialityRepo, SpecialityRepo>();
            services.AddScoped<ISpecialityService, SpecialityService>();
            
            services.AddScoped<ICollectionRepo, CollectionRepo>();
            services.AddScoped<ICollectionService, CollectionService>();

            services.AddScoped<IBuyService, BuyService>();
            services.AddScoped<IBuyRepo, BuyRepo>();

            services.AddScoped<IGanService, GanService>();
            services.AddScoped<IGanRepo, GanRepo>();

            services.AddScoped<ICurrentLoggedInService, CurrentLoggedInService>();

            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ISearchRepository, SearchRepository>();

            services.AddCors(options => {
                options.AddPolicy(name: _CORSPolicy,
                    builder => {
                        if (_env.IsDevelopment()) {
                            builder
                                .WithOrigins("http://localhost:3000") 
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();

                        }
                        else if(_env.IsStaging()){
                            builder.AllowAnyHeader()
                                .WithOrigins("https://haqq-staging-enviroment-f8b93911-clnxo.ondigitalocean.app")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        }
                        else
                        {
                            builder.AllowAnyHeader()
                                .WithMethods("POST", "GET", "PUT")
                                .WithOrigins("https://domain1.com", "https://domain2.com");
                        }
                    });
            });
            
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
            
            services.AddMvc(option => option.EnableEndpointRouting = false) ;
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = Configuration["Auth0:Audience"];
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`.
                    // Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            });

            
            // Run the following before migrations: set ASPNETCORE_ENVIRONMENT=Staging
            // set ASPNETCORE_ENVIRONMENT=Development
            var dbConfig = Configuration["Db-Connections:ConnectionDbString"];
            Console.Write(Configuration["Local-Property-AppSetting"]);
            Console.Write(dbConfig);
            services.AddDbContext<ArtistBlockDbContext>(opt => 
                opt.UseNpgsql(dbConfig)
            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (_env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            app.UseRouting();
            
            app.UseCors(_CORSPolicy);
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        } 
       
    }
}