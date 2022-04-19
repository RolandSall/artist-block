using account_service.Models;

namespace account_service.Repository.CollectionRepo;

public class CollectionRepo: ICollectionRepo
{
    private readonly ArtistBlockDbContext _context;

    public CollectionRepo(ArtistBlockDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Painting> GetCurrentLoggedInUserPaintingCollection(Guid currentLoggedInUserId)
    {
        var currentLoggedInUserPaintings =
            _context.Paintings.Where(painting => painting.RegisteredUserId.Equals(currentLoggedInUserId));
        return currentLoggedInUserPaintings;
    }

    public IEnumerable<Painting> GetPaintingCollectionByUserId(Guid userId)
    {
        var currentLoggedInUserPaintings =
            _context.Paintings.Where(painting => painting.RegisteredUserId.Equals(userId));
        return currentLoggedInUserPaintings;
    }

    public IEnumerable<Painting> GetCurrentPainterOwnedPaintings(Guid painterId)
    {
        var ownedPainting = _context.Paintings.Where(painting => painting.PainterId.Equals(painterId));
        return ownedPainting;
    }
}