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

    public void BuyPainting(Guid registeredUserId, Guid paintingId)
    {

        var painting = _context.Paintings.AsNoTracking().FirstOrDefault(painting => painting.PainterId.Equals(painting));

        painting.RegisteredUserId = registeredUserId;

        _context.Paintings.Update(painting);
        _context.SaveChanges();

    }
}