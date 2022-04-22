using account_service.ValueObjects;

namespace account_service.Repository.StatsRepo;

public class StatsRepo : IStatsRepo
{
    private readonly ArtistBlockDbContext _context;
    
    public StatsRepo (ArtistBlockDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<IdAndValue> GetNumPaintersByCountry()
    {
        var toReturn = _context.Painters.GroupBy(painter => painter.Location)
            .Select(group => new IdAndValue()
            {
                Id = group.Key,
                Value = group.Count(),
            }).ToList();

        return toReturn;
    }

    public IEnumerable<IdAndValue> GetNumPaintersAndUsers()
    {
        var totalUsers = _context.RegisteredUsers.Count();
        var painters = _context.Painters.Count();
        
        // exclusively Users = total - painters
        return new List<IdAndValue>()
        {
            new() {Id = "Client", Value = totalUsers - painters},
            new() {Id = "Painter", Value = painters}
        };
    }

    public NumPaintingsAndGan GetNumGanAndNormalPaintings()
    {
        var numGanImages = _context.GanGeneratedImages.Count();
        var numPaintings = _context.Paintings.Count();

        return new NumPaintingsAndGan{numGans = numGanImages , numPaintings = numPaintings};
    }
}