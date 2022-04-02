using account_service.Models;

namespace account_service.Repository.CreatePaintingRepo;

public interface ICreatePaintingRepo
{
    Painting CreatePainting(Painting painting , Guid painterId );
}