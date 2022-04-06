using account_service.Models;
using account_service.Repository.CollectionRepo;
using account_service.Repository.RegistrationRepo;

namespace account_service.Service.CollectionService;

public class CollectionService: ICollectionService
{
    private readonly ICollectionRepo _collectionRepo;
    private readonly IRegistrationRepo _registrationRepo;

    public CollectionService(ICollectionRepo collectionRepo, IRegistrationRepo registrationRepo)
    {
        _collectionRepo = collectionRepo;
        _registrationRepo = registrationRepo;
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
}