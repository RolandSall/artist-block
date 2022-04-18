using account_service.Controllers.Helper;
using account_service.Controllers.SearchController;
using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Service.SearchService;

public interface ISearchService
{
    SearchResult FilterRegisterPainterForHomePage(PainterSearchField painter);
    PagedList<Painting> FilterPainting(FindPaintingFilter hirePainterFilter, string auth0UserId);
}