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

        var painting = _context.Paintings.AsNoTracking().FirstOrDefault(painting => painting.PainterId.Equals(paintingId));

        if (painting == null)
        {
            throw new PaintingDoesNotExist("Painting Does Not Exist");
        }
        
        painting.RegisteredUserId = userId;

        _context.Paintings.Update(painting);
        _context.SaveChanges();

    }
}