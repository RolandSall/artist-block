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
    public ActionResult<ReadPaintingReviewDto> CreatePaintingReview(CreatePaintingReviewDto paintingReviewDto , Guid paintingId)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            var paintingReview = _mapper.Map<PaintingReview>(paintingReviewDto);
            var createdPaintingReview = _reviewService.CreatePaintingReview(paintingReview , paintingId , auth0UserId);
            
            return Ok(createdPaintingReview);
        }
        catch (PaintingReviewAlreadyPresentException exc)
        {
            Console.WriteLine(exc);
            return Problem(exc.Message);
        }
        catch (Exception exc)
        {
            return Problem(exc.Message);
        }
    }
    
    
    [HttpDelete]
    [Route("painting-review-delete/{Id:Guid}")]
    public ActionResult DeletePaintingReview( Guid Id )
    {
        try
        {
            _reviewService.DeletePaintingReview(Id);
        }
        catch (Exception exc)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpGet]
    [Route("painting-reviews/{paintingId:guid}")]
    public ActionResult<IEnumerable<ReadPaintingReviewDto>> GetPaintingReviews(Guid paintingId)
    {
        var paintingReviews = _reviewService.GetPaintingReviews(paintingId);
        
        return Ok(_mapper.Map<IEnumerable<ReadPaintingReviewDto>>(paintingReviews));
    }

    // Not used for now but kept 
    [HttpGet]
    [Route("painting-review/{paintingReviewId:guid}")]
    public ActionResult<ReadPaintingReviewDto> GetPaintingReview(Guid paintingReviewId)
    {
        var paintingReview = _reviewService.GetPaintingReviewById(paintingReviewId);
        return Ok(_mapper.Map<ReadPaintingReviewDto>(paintingReview));
    }
}