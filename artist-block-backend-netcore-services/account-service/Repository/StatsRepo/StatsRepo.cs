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
}