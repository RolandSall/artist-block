using System.Security.Claims;
using account_service.CustomException;
using account_service.DTO.Gan;
using account_service.Models;
using account_service.Service.GanService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.GanController;

[ApiController]
[Route("api/v1/account-service")]
public class GanController : ControllerBase
{
    private readonly IGanService _ganService;

    public GanController(IGanService ganService)
    {
        _ganService = ganService;
    }

    [HttpPost]
    [Route("gan-image/claim")]
    [Authorize]
    public ActionResult ClaimImage(ClaimGanImageDto claimGanImageDto)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // TODO: Create A Mapper Once The Object Becomes Bigger
            var paintingId = _ganService.ClaimGanImage(claimGanImageDto.Description, auth0UserId);
            //TODO: Same for Read Mapper
            var readClaimGanImageDto = new ReadClaimGanImageDto()
            {
                GanImageId = paintingId
            };
            return Ok(readClaimGanImageDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }

    [HttpPost]
    [Route("gan-image/upload/{ganPaintingId}")]
    [Authorize]
    public async Task<ActionResult> UploadImage(IFormFile image, Guid ganPaintingId)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ganService.UploadImage(image, auth0UserId, ganPaintingId);
            return Ok("Image Saved!");
        }
        catch (GanGeneratedImageNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }

    [HttpGet]
    [Route("gan-image/collection")]
    [Authorize]
    public ActionResult GetAllClaimedGanImagesForClient()
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ganImagesList = _ganService.GetAllClaimedGanImagesForClient(auth0UserId);
            //TODO: READ MAPPER IS NEEDED 
            return Ok(ganImagesList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }

    [HttpGet]
    [Route("gan-image/{ganImageId}")]
    public ActionResult<GanGeneratedImage> GetGanImageById(Guid ganImageId)
    {
        try
        {
            var ganImage = _ganService.GetGanImageById(ganImageId);
            return Ok(ganImage);
        }
        catch (ContentNotFoundById exc)
        {
            return NotFound(exc.Message);
        }
    }
        

}