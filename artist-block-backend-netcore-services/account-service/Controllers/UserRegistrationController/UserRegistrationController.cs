using System.Security.Claims;
using account_service.CustomException;
using account_service.DTO.Registration;
using account_service.Models;
using account_service.Repository.RegistrationRepo;
using account_service.Service.RegistrationService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.UserRegistrationController;

[Route("api/v1/account-service")]
[ApiController]     
public class UserRegistrationController: ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IMapper _mapper;

    public UserRegistrationController(IMapper mapper, IRegistrationService registrationService)
    {
        _mapper = mapper;
        _registrationService = registrationService;
    }
    
    [HttpPost]
    [Route("register-client")]
    [Authorize]
    public ActionResult RegisterClient(CreateClientDto clientDto)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registeredUser = _mapper.Map<RegisteredUser>(clientDto);
            var registerClient = _registrationService.RegisterClient(registeredUser, auth0UserId);
            var readClientDto = _mapper.Map<ReadClientDto>(registerClient);
            return Ok(readClientDto);
        }
        catch (ClientAlreadyExistException e)
        {
            return Conflict(e.message);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }
    
    [HttpPost]
    [Route("register-painter")]
    [Authorize]
    public ActionResult RegisterPainter(CreatePainterDto painterDto)
    {
        try
        {
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registeredPainter = _mapper.Map<Painter>(painterDto);
            var painter = _registrationService.RegisterPainter( registeredPainter, auth0UserId );
            var readPainterDto = _mapper.Map<ReadPainterDto>(painter);
            return Ok(readPainterDto);
        }
        catch (ClientAlreadyExistException e)
        {
            return Conflict(e.message);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
    
    
    [HttpGet]
    [Route("register-painter/{painterId}")]
    [Authorize]
    public ActionResult GetPainterById(Guid painterId)
    {
        try
        {
            var painter = _registrationService.GetPainterById(painterId);
            var readPainterDto = _mapper.Map<ReadPainterDto>(painter);
            return Ok(readPainterDto);
        }
        catch (ClientAlreadyExistException e)
        {
            return Conflict(e.message);
        }
        catch (RegistrationFailedException e)
        {
            return Problem(e.message);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.Message);
        }
    }
    
            
    [HttpPost]
    [Route("register-client/image")]
    /*[Authorize]*/
    public async Task<ActionResult> UploadImage(IFormFile image)
    {
        try
        {   
            var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _registrationService.UploadImage(image, auth0UserId);
            return Ok("Image Added");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }

}