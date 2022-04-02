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
        var registeredUserId = Guid.NewGuid();
        registeredUser.RegisteredUserId = registeredUserId;
        var savedRegisteredUser = _registrationRepo.RegisterClient(registeredUser,auth0UserId);
        return savedRegisteredUser;
    }

    public Painter RegisterPainter(Painter painter, string auth0UserId)
    {
        var painterId = Guid.NewGuid();
        // TODO : automatic add 
        //registeredPainter.RegisteredUserId = Guid.NewGuid();
        
        
        // Register as Client
        painter.PainterId = painterId;
        var registeredClient = RegisterClient(painter.RegisteredUser, auth0UserId);
        painter.RegisteredUserId = registeredClient.RegisteredUserId;
        
        // assign for each speciality a unique Id to to the db and give FK to the newly generated lawyerId
        foreach (var painterSpeciality in painter.PainterSpecialities)
        {
            Guid painterSpecialityId = Guid.NewGuid();;
            painterSpeciality.PainterSpecialityId = painterSpecialityId;
            painterSpeciality.PainterId = painterId;
        }
    
        // Register as painter
        var registeredPainter = _registrationRepo.RegisterPainter( painter );
        return registeredPainter;
    }
    
}