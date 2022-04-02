using account_service.Models;

namespace account_service.Service.PaintingService;

public interface IPaintingService
{
    Painting CreatePainting(Painting painting , Guid painterId );
    IEnumerable<Painting> GetPaintingsForPainter(Guid painterId);
}