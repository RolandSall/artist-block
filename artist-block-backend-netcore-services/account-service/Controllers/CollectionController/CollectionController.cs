using System.Security.Claims;
using account_service.CustomException;
using account_service.DTO.Painting;
using account_service.Service.CollectionService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.CollectionController;

[Route("api/v1/account-service")]
[ApiController]   
public class CollectionController: ControllerBase
{
    private readonly ICollectionService _collectionService;
    private readonly IMapper _mapper;

    public CollectionController(ICollectionService collectionService , IMapper mapper)
    {
        _collectionService = collectionService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("current/paintings")]
    [Authorize]
    public ActionResult<IEnumerable<ReadPaintingDto>> GetCurrentLoggedInUserPaintingCollection()
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentLoggedInUserPainting = _collectionService.GetCurrentLoggedInUserPaintingCollection(auth0UserId);
            var currentLoggedInUserPaintingDto = _mapper.Map<IEnumerable<ReadPaintingDto>>(currentLoggedInUserPainting);
            return Ok(currentLoggedInUserPaintingDto);
        }
        catch (UserNotFoundException e)
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
    [Route("current-painter/paintings")]
    [Authorize]
    public ActionResult<IEnumerable<ReadPaintingDto>> GetCurrentPainterOwnedPaintings()
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentLoggedInUserPainting = _collectionService.GetCurrentPainterOwnedPaintings(auth0UserId);
            var currentLoggedInUserPaintingDto = _mapper.Map<IEnumerable<ReadPaintingDto>>(currentLoggedInUserPainting);
            return Ok(currentLoggedInUserPaintingDto);
        }
        catch (UserNotFoundException e)
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
    [Route("collection/paintings/{userId}")]
    [Authorize]
    public ActionResult<IEnumerable<ReadPaintingDto>> GetPaintingCollectionByUserId(Guid userId)
    {
        try
        {
            var currentLoggedInUserPainting = _collectionService.GetPaintingCollectionByUserId(userId);
            var currentLoggedInUserPaintingDto = _mapper.Map<IEnumerable<ReadPaintingDto>>(currentLoggedInUserPainting);
            return Ok(currentLoggedInUserPaintingDto);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }
}