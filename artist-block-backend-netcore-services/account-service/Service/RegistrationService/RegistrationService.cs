using account_service.Models;
using account_service.Repository.RegistrationRepo;
using AutoMapper;

namespace account_service.Service.RegistrationService;

public class RegistrationService: IRegistrationService

{
    
    

    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IRegistrationRepo _registrationRepo;

    public RegistrationService(IConfiguration configuration, IMapper mapper, IRegistrationRepo registrationRepo)
    {
        _configuration = configuration;
        _mapper = mapper;
        _registrationRepo = registrationRepo;
    }


    public RegisteredUser RegisterClient(RegisteredUser registeredUser, string auth0UserId)
    {
        Guid registeredUserId = Guid.NewGuid();
        registeredUser.RegisteredUserId = registeredUserId;
        RegisteredUser savedRegisteredUser = _registrationRepo.RegisterClient(registeredUser,auth0UserId);
        return savedRegisteredUser;
    }
}