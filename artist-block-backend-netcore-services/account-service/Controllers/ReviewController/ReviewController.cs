using System.Security.Claims;
using account_service.CustomException;
using account_service.DTO.PaintingReview;
using account_service.Models;
using account_service.Service.ReviewService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("painting-review/{paintingId:guid}")]
    public ActionResult CreatePaintingReview(CreatePaintingReviewDto paintingReviewDto , Guid paintingId)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var paintingReview = _mapper.Map<PaintingReview>(paintingReviewDto);
            var createdPaintingReview = _reviewService.CreatePaintingReview(paintingReview, paintingId, auth0UserId);

            return Ok(_mapper.Map<ReadPaintingReviewDto>(createdPaintingReview));
        }
        catch (PaintingReviewAlreadyPresentException exc)
        {
            Console.WriteLine(exc);
            return Problem(exc.Message);
        }
        catch (ContentNotFoundById exc)
        {
            Console.WriteLine(exc);
            return Problem(exc.Message);
        }
        catch (Exception exc)
        {
            return Problem(exc.Message);
        }
    }

    [HttpGet]
    [Route("painting-reviews/{paintingId:guid}")]
    public ActionResult<IEnumerable<ReadPaintingReviewDto>> GetPaintingReviews(Guid paintingId)
    {
        try
        {
            var paintingReviews = _reviewService.GetPaintingReviews(paintingId);
            return Ok(_mapper.Map<IEnumerable<ReadPaintingReviewDto>>(paintingReviews));
        }
        catch (ContentNotFoundById exc)
        {
            return Problem(exc.Message);
        }
    }

    // Not used for now but kept 
    [HttpGet]
    [Route("painting-review/{paintingReviewId:guid}")]
    public ActionResult<ReadPaintingReviewDto> GetPaintingReview(Guid paintingReviewId)
    {
        var paintingReview = _reviewService.GetPaintingReviewById(paintingReviewId);
        return Ok(_mapper.Map<ReadPaintingReviewDto>(paintingReview));
    }
    
    [HttpDelete]
    [Route("painting-review-delete/{paintingReviewId:Guid}")]
    public ActionResult DeletePaintingReview( Guid paintingReviewId )
    {
        try
        {
            _reviewService.DeletePaintingReview(paintingReviewId);
        }
        catch (ContentNotFoundById exc)
        {
            return Problem(exc.Message);
        }

        return NoContent();
    }
}