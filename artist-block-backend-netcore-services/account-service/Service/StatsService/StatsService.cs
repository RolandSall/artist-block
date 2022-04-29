using account_service.Repository.StatsRepo;
using account_service.ValueObjects;

namespace account_service.Service.StatsService;

public class StatsService : IStatsService
{
    private readonly IStatsRepo _statsRepo;

    public StatsService(IStatsRepo statsRepo)
    {
        _statsRepo = statsRepo;
    }
    
    public IEnumerable<IdAndValue> GetNumPaintersByCountry()
    {
        return _statsRepo.GetNumPaintersByCountry();
    }

    public IEnumerable<IdAndValue> GetNumPaintersAndUsers()
    {
        return _statsRepo.GetNumPaintersAndUsers();
    }

    public NumPaintingsAndGan GetNumGanAndNormalPaintings()
    {
        return _statsRepo.GetNumGanAndNormalPaintings();
    }

    public IEnumerable<IdAndValue> GetNumPaintersBySpecialty()
    {
        return _statsRepo.GetNumPaintersBySpecialty();
    }
}