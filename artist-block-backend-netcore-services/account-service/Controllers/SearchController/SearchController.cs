using System.Security.Claims;
using System.Text.Json;
using account_service.Models;
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
    
    
    
    
    [HttpGet]
    [Route("paintings/search")]
    public ActionResult FilterPainting([FromQuery] FindPaintingFilter filter)
    {
        var auth0UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filteredPaintings = _searchService.FilterPainting(filter, auth0UserId);
        var metadata = new
        {
            filteredPaintings.TotalCount,
            filteredPaintings.PageSize,
            filteredPaintings.CurrentPage,
            filteredPaintings.TotalPages,
            filteredPaintings.HasNext,
            filteredPaintings.HasPrevious
        };
            
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        var readFilteredLawyersDtos = _mapper.Map<IEnumerable<Painting>>(filteredPaintings);
        return Ok(readFilteredLawyersDtos);
    }
}