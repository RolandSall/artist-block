using account_service.ValueObjects;

namespace account_service.Service.StatsService;

public interface IStatsService
{
    public IEnumerable<IdAndValue> GetNumPaintersByCountry();
    IEnumerable<IdAndValue> GetNumPaintersAndUsers();
    NumPaintingsAndGan GetNumGanAndNormalPaintings();
}