using System.Linq;
using account_service.Controllers.SearchController;
using account_service.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository.SearchRepo;

public class SearchRepository: ISearchRepository
{
    private readonly ArtistBlockDbContext _context;

    public SearchRepository(ArtistBlockDbContext context)
    {
        _context = context;
    }

    public SearchResult FilterRegisterPainterForHomePage(PainterSearchField searchCriteria)
    {
        string[] words = searchCriteria.SearchCriteria.Split(' ');
        var filterPainter = new List<PainterSearchHeader>().DefaultIfEmpty();
        
        var SearchResult = new SearchResult()
        {
            PaintingList = null,
            PainterList = null
        };

        switch (searchCriteria.Type)
        {
            case SearchCriteriaType.Painter:
            {
                if (words.Length == 1)
                {
                    filterPainter = _context.Painters
                        .Include(l => l.RegisteredUser)
                        .Where(l => l.RegisteredUser.FirstName.ToLower().StartsWith(searchCriteria.SearchCriteria.ToLower())
                                    | l.RegisteredUser.LastName.ToLower()
                                        .StartsWith(searchCriteria.SearchCriteria.ToLower())
                        )
                        .Select(x => new PainterSearchHeader()
                        {
                            PainterId = x.PainterId,
                            FirstName = x.RegisteredUser.FirstName,
                            LastName = x.RegisteredUser.LastName,
                        }).ToList();
                }
                else
                {
                    filterPainter = _context.Painters
                        .Include(l => l.RegisteredUser)
                        .Where(l => l.RegisteredUser.FirstName.ToLower().Equals(words[0].ToLower())
                                    && l.RegisteredUser.LastName.ToLower().StartsWith(words[1].ToLower())
                        )
                        .Select(x => new PainterSearchHeader()
                        {
                            PainterId = x.PainterId,
                            FirstName = x.RegisteredUser.FirstName,
                            LastName = x.RegisteredUser.LastName,
                        }).ToList();
                }

                List<PainterSearchHeader> sortedList = new List<PainterSearchHeader>();

                foreach (PainterSearchHeader painter in filterPainter)
                {
                    if (painter.FirstName[0].Equals(searchCriteria.SearchCriteria[0]))
                    {
                        sortedList.Insert(0, painter);
                    }
                    else
                    {
                        sortedList.Add(painter);
                    }
                }


                SearchResult = new SearchResult()
                {
                    PainterList = sortedList
                };
                return SearchResult;
            }
            case SearchCriteriaType.Painting:
            {
                var filterPainting = _context.Paintings
                    .Where(painting => painting.PaintingName.ToLower().StartsWith(searchCriteria.SearchCriteria.ToLower())
                                       | painting.PaintedYear.ToLower().StartsWith(searchCriteria.SearchCriteria.ToLower())
                    )
                    .Select(x => new PaintingSearchHeader()
                    {
                        PaintingId = x.PainterId,
                        PaintingName = x.PaintingName,
                        PaintingYear = x.PaintedYear,
                    }).ToList();
           
                List<PaintingSearchHeader> sortedList = new List<PaintingSearchHeader>();

                foreach (PaintingSearchHeader paintingEntry in filterPainting)
                {
                    if (paintingEntry.PaintingName[0].Equals(searchCriteria.SearchCriteria[0]))
                    {
                        sortedList.Insert(0, paintingEntry);
                    }
                    else
                    {
                        sortedList.Add(paintingEntry);
                    }
                }

                SearchResult = new SearchResult()
                {
                    PaintingList = sortedList,
                
                };

                return SearchResult;
            }
            default:
                return SearchResult;
        }
    }
}