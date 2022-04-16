using account_service.Models;
using AutoMapper;

namespace account_service.Repository.ReviewRepo;

public class ReviewRepo : IReviewRepo
{
    private readonly ArtistBlockDbContext _context;
    private readonly IMapper _mapper;

    public ReviewRepo(ArtistBlockDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PaintingReview CreatePaintingReview(PaintingReview paintingReview)
    {
        //TODO: make sure the registeredUserId did not already comment on this painting 
        
        var review = _context.PaintingReview.Add(paintingReview).Entity;
        _context.SaveChanges();
        
        return review;
    }
}