using account_service.Models;
using account_service.Repository.PaintingRepo;

namespace account_service.Service.PaintingService;

public class PaintingService : IPaintingService
{
    private readonly IPaintingRepo _paintingRepo;
    
    public PaintingService(IPaintingRepo paintingRepo)
    {
        _paintingRepo = paintingRepo;
    }   
    
    public Painting CreatePainting(Painting painting , Guid painterId )
    {            
        painting.PaintingId = Guid.NewGuid();
        painting.PainterId = painterId;
        painting.RegisteredUserId = null; // no buyer yet

        var createdPainting = _paintingRepo.CreatePainting(painting , painterId);

        return createdPainting;
    }

    public IEnumerable<Painting> GetPaintingsForPainter(Guid painterId)
    {
        var paintings = _paintingRepo.GetPaintingsForPainter(painterId);

        return paintings;
    }
}