using account_service.Models;

namespace account_service.Repository.PaintingRepo;

public interface IPaintingRepo
{
    Painting CreatePainting(Painting painting , Guid painterId );
    IEnumerable<Painting> GetPaintingsForPainter(Guid painterId);
}