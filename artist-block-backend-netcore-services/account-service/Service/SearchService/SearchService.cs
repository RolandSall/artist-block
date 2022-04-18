using account_service.Controllers.Helper;
using account_service.Controllers.SearchController;
using account_service.Models;
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


    public SearchResult FilterRegisterPainterForHomePage(PainterSearchField painter)
    {
       return _searchRepository.FilterRegisterPainterForHomePage(painter);
     
    }


        public PagedList<Painting> FilterPainting(FindPaintingFilter filter, string auth0UserId)
        {
            return _searchRepository.FilterPainting(filter, auth0UserId);
        }
    
}