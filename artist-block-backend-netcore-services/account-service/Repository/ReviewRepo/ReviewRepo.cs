using account_service.CustomException;
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
        // make sure the registeredUserId did not already comment on this painting 
        var present = _context.PaintingReview.First(review => review.RegisteredUserId == paintingReview.RegisteredUserId);

        if (present != null)
            throw new PaintingReviewAlreadyPresentException("the user has already commented on this particular painting");

        var review = _context.PaintingReview.Add(paintingReview).Entity;
        _context.SaveChanges();
        
        return review;
    }

    public IEnumerable<PaintingReview> GetPaintingReviews(Guid paintingId)
    {
        var reviews = _context.PaintingReview.Where(review => review.PaintingId == paintingId).ToList();
        return reviews;
    }

    public PaintingReview GetPaintingReviewById(Guid paintingReviewId)
    {
        var review = _context.PaintingReview.First(paintingReview => paintingReview.PaintingReviewId == paintingReviewId);
        return review;
    }

    public void DeletePaintingReview(Guid Id)
    {
        var toRemove = _context.PaintingReview.First(review => review.PaintingReviewId == Id);

        if (toRemove == null)
            throw new ContentNotFoundById($"painting review with {Id} does not exist!");
        
        _context.PaintingReview.Remove(toRemove);
        _context.SaveChanges();
    }
}