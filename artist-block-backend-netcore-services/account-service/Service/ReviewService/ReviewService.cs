using account_service.DTO.PaintingReview;
using account_service.Models;
using account_service.Repository.RegistrationRepo;
using account_service.Repository.ReviewRepo;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Service.ReviewService;

public class ReviewService : IReviewService
{
    private readonly IMapper _mapper;
    private readonly IReviewRepo _reviewRepo;
    
    private readonly IRegistrationRepo _registrationRepo;

    public ReviewService(IMapper mapper, IReviewRepo reviewRepo, IRegistrationRepo registrationRepo)
    {
        _mapper = mapper;
        _reviewRepo = reviewRepo;
        _registrationRepo = registrationRepo;
    }

    public PaintingReview CreatePaintingReview(PaintingReview paintingReview, Guid paintingId, string auth0Id)
    {
        paintingReview.PaintingReviewId = Guid.NewGuid();
        paintingReview.Timestamp = DateTime.Now;
        paintingReview.PaintingId = paintingId;
        paintingReview.RegisteredUserId = _registrationRepo.GetUserInformation(auth0Id).RegisteredUserId;

        _reviewRepo.CreatePaintingReview(paintingReview);

        return paintingReview;
    }

    public IEnumerable<PaintingReview> GetPaintingReviews(Guid paintingId)
    {
        var paintingReviews = _reviewRepo.GetPaintingReviews(paintingId);
        return paintingReviews;
    }

    public PaintingReview GetPaintingReviewById(Guid paintingReviewId)
    {
        var paintingReview = _reviewRepo.GetPaintingReviewById(paintingReviewId);
        return paintingReview;
    }
}