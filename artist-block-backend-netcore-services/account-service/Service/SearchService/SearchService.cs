using account_service.Controllers.SearchController;
using account_service.Repository.SearchRepo;
using account_service.ValueObjects;

namespace account_service.Service.SearchService;

public class SearchService: ISearchService
{

    private readonly ISearchRepository _searchRepository;

    public SearchService(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }


    public PainterSearchResult FilterRegisterPainterForHomePage(PainterSearchField painter)
    {
       var x = _searchRepository.FilterRegisterPainterForHomePage(painter);
       return x;
    }
}