using account_service.DTO.Painting;
using account_service.Models;
using account_service.Repository.CreatePaintingRepo;
using account_service.Service.CreatePaintingService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.CreatePaintingController;

[Route("api/v1")]
[ApiController]
public class CreatePaintingController : ControllerBase
{
    private readonly ICreatePaintingService _createPaintingService;
    private readonly IMapper _mapper;

    public CreatePaintingController(IMapper mapper, ICreatePaintingService createPaintingService)
    {
        _mapper = mapper;
        _createPaintingService = createPaintingService;
    }

    [HttpPost]
    [Route("create-painting/{painterId:guid}")]
    // [Authorize] just for testing
    public ActionResult CreatePainting(CreatePaintingDto paintingDto , Guid painterId )
    {
        try
        {
            var painting = _mapper.Map<Painting>(paintingDto);
            var createdPainting = _createPaintingService.CreatePainting(painting, painterId);
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
}