using account_service.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace account_service.Repository.PaintingRepo;

public class PaintingRepo : IPaintingRepo
{
    private readonly ArtistBlockDbContext _context;
    private IPaintingRepo _paintingRepoImplementation;

    public PaintingRepo(ArtistBlockDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public Painting CreatePainting(Painting painting , Guid painterId)
    {
        // make sure the painting's painter exists
        var painter = _context.Painters.FirstOrDefault(painter => painter.PainterId.Equals(painterId));
        
        //TODO: handle the case where there is an identical painting in the db

        if (painter == null)
            throw new PainterDoesNotExistException("does not exist");
        
        // painting can be added safely
        var createdPainting = _context.Paintings.Add(painting).Entity;

        _context.SaveChanges();
        return createdPainting;
    }

    public IEnumerable<Painting> GetPaintingsForPainter(Guid painterId)
    {
        var paintings = _context.Paintings.Where(painter => painter.PainterId.Equals(painterId)).ToList();

        return paintings;
    }

    public Painting GetPaintingInformation(Guid paintingId)
    {
        var painting = _context.Paintings
            .FirstOrDefault(painting => painting.PaintingId.Equals(paintingId));

        return painting;
    }

    public Task AddImageReference(Painting currentPainting, string url)
    {
        currentPainting.PaintingUrl = url;
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public IEnumerable<Painting> GetNRandomPaintingsForSale(int number)
    {
        var rnd = new Random();
        
        // TODO: might cause a problem when many entries in the DB exist ???
        var paintings = _context.Paintings.Where(painting => painting.PaintingStatus.Equals("For Sale")).AsEnumerable()
            // source of snippet below : https://stackoverflow.com/questions/48087/select-n-random-elements-from-a-listt-in-c-sharp
            .OrderBy( token => rnd.Next() ).Take(number).ToList();

        return paintings;
    }
}