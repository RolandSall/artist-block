using account_service.Models;

namespace account_service.Repository.CollectionRepo;

public interface ICollectionRepo
{
    IEnumerable<Painting> GetCurrentLoggedInUserPaintingCollection(Guid currentLoggedInUserId);
    IEnumerable<Painting> GetPaintingCollectionByUserId(Guid userId);
    IEnumerable<Painting> GetCurrentPainterOwnedPaintings(Guid painterId);
}