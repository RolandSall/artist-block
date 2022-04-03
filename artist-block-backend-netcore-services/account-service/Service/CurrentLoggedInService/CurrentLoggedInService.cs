using account_service.Models;
using account_service.Repository.RegistrationRepo;
using account_service.ValueObjects;
using AutoMapper;

namespace account_service.Service.CurrentLoggedInService;

public class CurrentLoggedInService: ICurrentLoggedInService
{
    private readonly IConfiguration _configuration;
    private readonly IRegistrationRepo _registrationRepo;

    public CurrentLoggedInService(IConfiguration configuration, IMapper mapper, IRegistrationRepo registrationRepo)
    {
        _configuration = configuration;
        _registrationRepo = registrationRepo;
    }

    public CurrentUser GetCurrentLoggedInUser(string auth0UserId)
    {
        var currentLoggedInUser = _registrationRepo.GetCurrentLoggedInUser(auth0UserId);
        return currentLoggedInUser;
    }

    public Painter GetCurrentLoggedInPainterInfo(string auth0UserId)
    {
        var painterId = _registrationRepo.GetCurrentLoggedInPainterInfo(auth0UserId);
        return _registrationRepo.GetPainterById(painterId);
    }
}