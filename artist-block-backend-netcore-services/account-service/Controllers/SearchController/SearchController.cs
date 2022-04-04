using account_service.Service.SearchService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.SearchController;

[Microsoft.AspNetCore.Components.Route("api/v1/account-service")]
[ApiController]    

public class SearchController: ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly IMapper _mapper;

    public SearchController(ISearchService searchService,IMapper mapper)
    {
        _searchService = searchService;
        _mapper = mapper;
    }
   
    [HttpGet]
    [Route("registered-painter/home-search")]
    public ActionResult FilterRegisterPainterForHomePage([FromQuery] PainterSearchField painter)
    {

        var filteredRegisteredLawyers = _searchService.FilterRegisterPainterForHomePage(painter);
        return Ok(filteredRegisteredLawyers);
    }
}