using System.Security.Claims;
using account_service.DTO.Registration;
using account_service.Models;
using account_service.Repository.RegistrationRepo;
using account_service.Service.RegistrationService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.UserRegistrationController;

[Route("api/v1/")]
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
}