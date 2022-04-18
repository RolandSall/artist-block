using account_service.Service.StatsService;
using account_service.ValueObjects;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.StatsController;

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
    public ActionResult<IEnumerable<PaintersByCountry>> GetNumPaintersByCountry()
    {
        var entries = _statsService.GetNumPaintersByCountry();
        return Ok(entries);
    }

    [HttpGet]
    [Route("stats-users")]
    public ActionResult GetNumPaintersAndUsers()
    {
        // do not double count painters
        return Ok();
    }

    [HttpGet]
    [Route("stats-paintings")]
    public ActionResult GetNumGanAndNormalPaintings()
    {
        return Ok();
    }
}