using account_service.Controllers.SearchController;
using account_service.ValueObjects;

namespace account_service.Service.SearchService;

public interface ISearchService
{
    SearchResult FilterRegisterPainterForHomePage(PainterSearchField painter);
}