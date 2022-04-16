using System.Security.Claims;
using account_service.DTO.PaintingReview;
using account_service.Models;
using account_service.Service.ReviewService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.ReviewController;

public class ReviewController : ControllerBase
{

    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;
    
    public ReviewController(IReviewService reviewService, IMapper mapper)
    {
        _reviewService = reviewService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("review-painting/{paintingId:guid}")]
    public ActionResult<ReadPaintingReviewDto> CreatePaintingReview(CreatePaintingReviewDto paintingReviewDto , Guid paintingId)
    {
        var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var paintingReview = _mapper.Map<PaintingReview>(paintingReviewDto);
        var createdPaintingReview = _reviewService.CreatePaintingReview(paintingReview , paintingId , auth0UserId);
        return Ok(createdPaintingReview);
    }
    
}