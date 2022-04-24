using System.Security.Claims;
using account_service.CustomException;
using account_service.DTO.Current;
using account_service.DTO.Registration;
using account_service.Service.CurrentLoggedInService;
using account_service.ValueObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.CurrentLoggedInController;

[Route("api/v1/account-service")]
[ApiController]   
public class CurrentLoggedInController: ControllerBase
{
    private readonly ICurrentLoggedInService _currentLoggedInService;
    private readonly IMapper _mapper;

    public CurrentLoggedInController(ICurrentLoggedInService currentLoggedInService, IMapper mapper)
    {
        _currentLoggedInService = currentLoggedInService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    [Route("current-painter")]
    [Authorize]
    public ActionResult GetCurrentLoggedInPainterInfo()
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registeredPainter = _currentLoggedInService.GetCurrentLoggedInPainterInfo(auth0UserId);
            var readPainterDto = _mapper.Map<ReadPainterDto>(registeredPainter);
            return Ok(readPainterDto);
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
    [Route("current")]
    [Authorize]
    public ActionResult GetCurrentLoggedInUser()
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentLoggedInUser = _currentLoggedInService.GetCurrentLoggedInUser(auth0UserId);
            var readCurrentUserDto = _mapper.Map<CurrentUser, ReadCurrentLoggedInUserDto>(currentLoggedInUser);
            return Ok(readCurrentUserDto);
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