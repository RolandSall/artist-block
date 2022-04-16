using account_service.DTO.PaintingReview;
using account_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Service.ReviewService;

public interface IReviewService
{
    PaintingReview CreatePaintingReview(PaintingReview paintingReview, Guid paintingId, string auth0Id);
    IEnumerable<PaintingReview> GetPaintingReviews(Guid paintingId);
    PaintingReview GetPaintingReviewById(Guid paintingReviewId);
}