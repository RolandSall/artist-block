using account_service.ValueObjects;

namespace account_service.Repository.StatsRepo;

public class StatsRepo : IStatsRepo
{
    private readonly ArtistBlockDbContext _context;
    
    public StatsRepo (ArtistBlockDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<PaintersByCountry> GetNumPaintersByCountry()
    {
        var toReturn = _context.Painters.GroupBy(painter => painter.Location)
            .Select(group => new PaintersByCountry()
            {
                Location = group.Key,
                Count = group.Count(),
            }).ToList();

        return toReturn;
    }

    public NumPaintersAndUsers GetNumPaintersAndUsers()
    {
        var totalUsers = _context.RegisteredUsers.Count();
        var painters = _context.Painters.Count();
        
        // exclusively Users = total - painters
        return new NumPaintersAndUsers {numPainters = painters, numUsers = totalUsers - painters};
    }

    public NumPaintingsAndGan GetNumGanAndNormalPaintings()
    {
        var numGanImages = _context.GanGeneratedImages.Count();
        var numPaintings = _context.Paintings.Count();

        return new NumPaintingsAndGan{numGans = numGanImages , numPaintings = numPaintings};
    }
}