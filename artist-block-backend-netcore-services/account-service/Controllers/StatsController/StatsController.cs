using account_service.Service.StatsService;
using account_service.ValueObjects;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.StatsController;

[Route("api/v1")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;
    private readonly IMapper _mapper;

    public StatsController(IMapper mapper, IStatsService statsService)
    {
        _mapper = mapper;
        _statsService = statsService;
    }

    [HttpGet]
    [Route("stats-country")]
    public ActionResult<IEnumerable<IdAndValue>> GetNumPaintersByCountry()
    {
        var entries = _statsService.GetNumPaintersByCountry();
        return Ok(entries);
    }

    [HttpGet]
    [Route("stats-users")]
    public ActionResult<IEnumerable<IdAndValue>> GetNumPaintersAndUsers()
    {
        var toReturn = _statsService.GetNumPaintersAndUsers();
        return Ok(toReturn);
    }

    [HttpGet]
    [Route("stats-paintings")]
    public ActionResult<NumPaintingsAndGan> GetNumGanAndNormalPaintings()
    {
        var toReturn = _statsService.GetNumGanAndNormalPaintings();
        return Ok(toReturn);
    }

    [HttpGet]
    [Route("stats-specialty")]
    public ActionResult<IEnumerable<IdAndValue>> GetNumPaintersBySpecialty()
    {
        var toReturn = _statsService.GetNumPaintersBySpecialty();
        return Ok(toReturn);
    }
}