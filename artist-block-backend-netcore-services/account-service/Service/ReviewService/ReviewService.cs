using account_service.Models;
using account_service.Repository.RegistrationRepo;
using account_service.Repository.ReviewRepo;
using AutoMapper;

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
        //TODO: add try catch for the love of god!!
        paintingReview.PaintingReviewId = Guid.NewGuid();
        paintingReview.Timestamp = DateTime.Now;
        paintingReview.PaintingId = paintingId;
        paintingReview.RegisteredUserId = _registrationRepo.GetUserInformation(auth0Id).RegisteredUserId;

        _reviewRepo.CreatePaintingReview(paintingReview);

        return paintingReview;
    }
}