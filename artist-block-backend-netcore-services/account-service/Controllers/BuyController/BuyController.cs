using System.Security.Claims;
using account_service.CustomException;
using account_service.DTO.Painting;
using account_service.Models;
using account_service.Repository.PaintingRepo;
using account_service.Service.BuyController;
using account_service.Service.PaintingService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.BuyController;

[Route("api/v1/account-service")]
[ApiController]
public class BuyController: ControllerBase
{
    private readonly IBuyService _buyService;
    private readonly IMapper _mapper;

    public BuyController(IMapper mapper, IBuyService buyService)
    {
        _mapper = mapper;
        _buyService = buyService;
    }
    
    
    [HttpPost]
    [Route("buy/{paintingId}")]
    [Authorize]
    public ActionResult BuyPainting(Guid paintingId)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _buyService.BuyPainting(paintingId, auth0UserId);
            return Ok("Success");
        }
        catch (PainterDoesNotExistException e)
        {
            return Problem(e.Message);
        }
        catch (PaintingDoesNotExist e)
        {
            return NotFound(e.Message);

        }
        catch (Exception other)
        {
            return Problem(other.Message);
        }
    }
    
    [HttpPost]
    [Route("sell/{paintingId}")]
    [Authorize]
    public ActionResult SellPainting(Guid paintingId, PaintingStatusRequest request)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _buyService.SellPainting(paintingId, auth0UserId, request);
            return Ok("Status Updated");
        }
        catch (PainterDoesNotExistException e)
        {
            return Problem(e.Message);
        }
        catch (PaintingDoesNotExist e)
        {
            return NotFound(e.Message);

        }
        catch (Exception other)
        {
            return Problem(other.Message);
        }
    }
    
}