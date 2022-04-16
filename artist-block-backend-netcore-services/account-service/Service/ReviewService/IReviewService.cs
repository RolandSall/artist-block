using account_service.Models;

namespace account_service.Service.ReviewService;

public interface IReviewService
{
    PaintingReview CreatePaintingReview(PaintingReview paintingReview, Guid paintingId, string auth0Id);
}