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

    public IEnumerable<IdAndValue> GetNumPaintersBySpecialty()
    {
        var toReturn = (from psp in _context.PainterSpecialities
            join sp in _context.Specialities on psp.SpecialityId equals sp.SpecialityId
            join p in _context.Painters on psp.PainterId equals p.PainterId
            group psp by sp.SpecialityType
            into newGroup
            select new IdAndValue()
            {
                Id = newGroup.Key,
                Value = newGroup.Count(),
            });
        
        return toReturn;
    }
}