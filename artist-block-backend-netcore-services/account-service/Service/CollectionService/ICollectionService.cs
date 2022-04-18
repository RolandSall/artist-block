using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Service.CollectionService;

public interface ICollectionService
{
    IEnumerable<Painting> GetCurrentLoggedInUserPaintingCollection(string auth0UserId);
    IEnumerable<Painting> GetPaintingCollectionByUserId(Guid userId);
    IEnumerable<Painting> GetCurrentPainterOwnedPaintings(string auth0UserId);
}