using account_service.Models;
using account_service.Repository.CollectionRepo;
using account_service.Repository.RegistrationRepo;
using account_service.Service.CurrentLoggedInService;

namespace account_service.Service.CollectionService;

public class CollectionService: ICollectionService
{
    private readonly ICollectionRepo _collectionRepo;
    private readonly IRegistrationRepo _registrationRepo;
    private readonly ICurrentLoggedInService _currentLoggedInService;

    public CollectionService(ICollectionRepo collectionRepo, IRegistrationRepo registrationRepo, ICurrentLoggedInService reCurrentLoggedInService)
    {
        _collectionRepo = collectionRepo;
        _registrationRepo = registrationRepo;
        _currentLoggedInService = reCurrentLoggedInService;
    }


    public IEnumerable<Painting> GetCurrentLoggedInUserPaintingCollection(string auth0UserId)
    {
        
        var currentLoggedInUserId = _registrationRepo.GetUserInfromation(auth0UserId).RegisteredUserId;
        return _collectionRepo.GetCurrentLoggedInUserPaintingCollection(currentLoggedInUserId);
    }

    public IEnumerable<Painting> GetPaintingCollectionByUserId(Guid userId)
    {
        return _collectionRepo.GetPaintingCollectionByUserId(userId);
    }

    public IEnumerable<Painting> GetCurrentPainterOwnedPaintings(string auth0UserId)
    {
        var painterId = _currentLoggedInService.GetCurrentLoggedInPainterInfo(auth0UserId).PainterId;
        return _collectionRepo.GetCurrentPainterOwnedPaintings(painterId);
    }
}