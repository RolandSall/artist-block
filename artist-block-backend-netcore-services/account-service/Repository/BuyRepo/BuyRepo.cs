using account_service.Controllers.BuyController;
using account_service.CustomException;
using account_service.Repository.PaintingRepo;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository.BuyRepo;

public class BuyRepo: IBuyRepo
{

    private readonly ArtistBlockDbContext _context;
    private readonly IMapper _mapper;

    public BuyRepo(ArtistBlockDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void BuyPainting(Guid paintingId, Guid userId)
    {

        var painting = _context.Paintings.AsNoTracking().FirstOrDefault(painting => painting.PaintingId.Equals(paintingId));

        if (painting == null)
        {
            throw new PaintingDoesNotExist("Painting Does Not Exist");
        }
        
        painting.RegisteredUserId = userId;

        _context.Paintings.Update(painting);
        _context.SaveChanges();

    }

    public void SellPainting(Guid paintingId, Guid userId, PaintingStatusRequest request)
    {
        var GetPaintingToBeUpdatedForPainter = _context.Paintings.AsNoTracking()
            .FirstOrDefault(painting => painting.PaintingId.Equals(paintingId)
                                        && painting.PainterId.Equals(userId)
            );
        

        if (GetPaintingToBeUpdatedForPainter == null)
        {
            throw new PaintingDoesNotExist("Painting Does Not Exist");
        }
        
        GetPaintingToBeUpdatedForPainter.PaintingStatus = request.Status;

        if (request.Price != null)
        {
            GetPaintingToBeUpdatedForPainter.PaintingPrice = request.Price;
            GetPaintingToBeUpdatedForPainter.BuyTimeStamp = request.BuyTimeStamp;
        }

        _context.Paintings.Update(GetPaintingToBeUpdatedForPainter);
        _context.SaveChanges();
    }
}