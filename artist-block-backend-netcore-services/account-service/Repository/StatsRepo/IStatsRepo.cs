using account_service.ValueObjects;

namespace account_service.Repository.StatsRepo;

public interface IStatsRepo
{
    IEnumerable<PaintersByCountry> GetNumPaintersByCountry();
    NumPaintersAndUsers GetNumPaintersAndUsers();
    NumPaintingsAndGan GetNumGanAndNormalPaintings();
}