using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository;


public class ArtistBlockDbContext: DbContext {
        public ArtistBlockDbContext(DbContextOptions<ArtistBlockDbContext> options): base(options){
                
      
            
        }
        public DbSet<Models.Painter> Painters { get; set; }
        public DbSet<Models.RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Models.AuthUser> AuthUsers { get; set; }
        public DbSet<Models.Speciality> Specialities { get; set; }
        public DbSet<Models.PainterSpeciality> PainterSpecialities { get; set; }
        public DbSet<Models.GanGeneratedImage> GanGeneratedImages { get; set; }
        
        public DbSet<Models.Painting> Paintings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
}