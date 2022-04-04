using System.Security.Claims;
using account_service.DTO.Painting;
using account_service.Models;
using account_service.Repository.PaintingRepo;
using account_service.Service.PaintingService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.PaintingController;

[Route("api/v1")]
[ApiController]
public class CreatePaintingController : ControllerBase
{
    private readonly IPaintingService _paintingService;
    private readonly IMapper _mapper;

    public CreatePaintingController(IMapper mapper, IPaintingService paintingService)
    {
        _mapper = mapper;
        _paintingService = paintingService;
    }

    [HttpPost]
    [Route("create-painting/{painterId}")]
    [Authorize]
    
    //TODO: get id from Auth0, not urgent
    public ActionResult CreatePainting(CreatePaintingDto paintingDto , Guid painterId )
    {
        try
        {
            var painting = _mapper.Map<Painting>(paintingDto);
            var createdPainting = _paintingService.CreatePainting(painting, painterId);
            var readPaintingDto = _mapper.Map<ReadPaintingDto>(createdPainting);
            return Ok(readPaintingDto);
        }
        catch (PainterDoesNotExistException e)
        {
            return Problem(e.Message);
        }
        catch (Exception other)
        {
            return Problem(other.Message);
        }
    }

    // TODO: add try and catch please
    [HttpGet]
    [Route("paintings/{painterId}")]
    public ActionResult<IEnumerable<ReadPaintingDto>> GetPaintingsForPainter( Guid painterId )
    {
        var paintings = _paintingService.GetPaintingsForPainter(painterId);
        
        var paintingDto = _mapper.Map<IEnumerable<ReadPaintingDto>>(paintings);

        return Ok(paintingDto);
    }
    
    //TODO: get 3 random paintings endpoint that are not sold
    
    
    [HttpPost]
    [Route("register-client/image/{paintingId}")]
    [Authorize]
    public async Task<ActionResult> UploadImage(IFormFile image, Guid paintingId)
    {
        try
        {   
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _paintingService.UploadImage(image, auth0UserId, paintingId);
            return Ok("Image Added");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }

}