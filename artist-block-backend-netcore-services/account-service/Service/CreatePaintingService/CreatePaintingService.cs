using account_service.Models;
using account_service.Repository.CreatePaintingRepo;

namespace account_service.Service.CreatePaintingService;

public class CreatePaintingService : ICreatePaintingService
{
    private readonly ICreatePaintingRepo _createPaintingRepo;
    
    public CreatePaintingService(ICreatePaintingRepo createPaintingRepo)
    {
        _createPaintingRepo = createPaintingRepo;
    }   
    
    public Painting CreatePainting(Painting painting , Guid painterId )
    {            
        painting.PaintingId = Guid.NewGuid();
        painting.PainterId = painterId;
        painting.RegisteredUserId = null; // no buyer yet
        
        var createdPainting = _createPaintingRepo.CreatePainting(painting , painterId);

        return createdPainting;
    }
}