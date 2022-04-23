using account_service.ValueObjects;

namespace account_service.Repository.StatsRepo;

public interface IStatsRepo
{
    IEnumerable<IdAndValue> GetNumPaintersByCountry();
    IEnumerable<IdAndValue> GetNumPaintersAndUsers();
    NumPaintingsAndGan GetNumGanAndNormalPaintings();
    IEnumerable<IdAndValue> GetNumPaintersBySpecialty();
}