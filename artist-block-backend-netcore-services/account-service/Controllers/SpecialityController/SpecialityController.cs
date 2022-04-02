using System.Security.Claims;
using account_service.DTO.Registration;
using account_service.DTO.Speciality;
using account_service.Models;
using account_service.Repository.RegistrationRepo;
using account_service.Service.RegistrationService;
using account_service.Service.SpecialityService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.SpecialityController;

[ApiController]
[Route("api/v1/account-service")]
public class SpecialityController: ControllerBase
{
    private readonly ISpecialityService _specialityService;
    private readonly IMapper _mapper;


    public SpecialityController(ISpecialityService specialityService, IMapper mapper)
    {
        _specialityService = specialityService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("speciality")]
    [Authorize]
    public ActionResult AddSpeciality(CreateSpecialityDto specialityDto)
    {
        try
        {
         
            var speciality = _mapper.Map<Speciality>(specialityDto);
            var registerClient = _specialityService.AddSpeciality(speciality);
            var readClientDto = _mapper.Map<ReadSpecialityDto>(registerClient);
            return Ok(readClientDto);
        }
        catch (SpecialityAlreadyExistException e)
        {
            return Conflict(e.message);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }
    
    [HttpGet]
    [Route("specialities")]
    [Authorize]
    public ActionResult GetAllSpecialities()
    {
        try
        {
         
           
            var registerClient = _specialityService.GetAllSpecialities();
            var readClientDto = _mapper.Map<ReadSpecialityDto>(registerClient);
            return Ok(readClientDto);
        }
        catch (SpecialityAlreadyExistException e)
        {
            return Conflict(e.message);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Problem(e.GetBaseException().ToString());
        }
    }
}