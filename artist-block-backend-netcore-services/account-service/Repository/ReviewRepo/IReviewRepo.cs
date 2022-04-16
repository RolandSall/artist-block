using account_service.Models;

namespace account_service.Repository.ReviewRepo;

public interface IReviewRepo
{
    public PaintingReview CreatePaintingReview(PaintingReview paintingReview);
}