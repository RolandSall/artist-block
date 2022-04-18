using account_service.CustomException;
using account_service.DTO.PaintingReview;
using account_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Repository.ReviewRepo;

public interface IReviewRepo
{
    public PaintingReview CreatePaintingReview(PaintingReview paintingReview);
    IEnumerable<PaintingReview> GetPaintingReviews(Guid paintingId);
    PaintingReview GetPaintingReviewById(Guid paintingReviewId);
    void DeletePaintingReview(Guid paintingReviewId);
}