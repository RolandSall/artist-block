using account_service.Models;

namespace account_service.Service.CreatePaintingService;

public interface ICreatePaintingService
{
    Painting CreatePainting(Painting painting , Guid painterId );
}