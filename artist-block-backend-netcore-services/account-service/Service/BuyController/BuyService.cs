using account_service.Repository.BuyRepo;
using account_service.Service.CurrentLoggedInService;
using account_service.Service.RegistrationService;
using Microsoft.EntityFrameworkCore.Internal;

namespace account_service.Service.BuyController;

public class BuyService: IBuyService
{
    
    private readonly IBuyRepo _buyRepo;
    private readonly ICurrentLoggedInService _currentLoggedInService;

    public BuyService(IBuyRepo buyRepo, ICurrentLoggedInService currentLoggedInService)
    {
        _buyRepo = buyRepo;
        _currentLoggedInService = currentLoggedInService;
    }

    public void BuyPainting(Guid paintingId, string auth0UserId)
    {
        var currentUser = _currentLoggedInService.GetCurrentLoggedInUser(auth0UserId);
        _buyRepo.BuyPainting(paintingId, currentUser.RegisteredUser.RegisteredUserId);
    }
}