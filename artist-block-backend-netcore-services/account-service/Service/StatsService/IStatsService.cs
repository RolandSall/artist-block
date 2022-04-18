using account_service.ValueObjects;

namespace account_service.Service.StatsService;

public interface IStatsService
{
    public IEnumerable<PaintersByCountry> GetNumPaintersByCountry();
    Tuple<int,int> GetNumPaintersAndUsers();
    Tuple<int,int> GetNumGanAndNormalPaintings();
}