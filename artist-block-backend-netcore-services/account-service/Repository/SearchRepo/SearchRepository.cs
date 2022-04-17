﻿using System.Linq;
using account_service.Controllers.Helper;
using account_service.Controllers.SearchController;
using account_service.Models;
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

    public PagedList<Painting> FilterPainting(FindPaintingFilter filter, string auth0UserId)
    {
        var auth0 = _context.AuthUsers
            .FirstOrDefault(auth => auth.Auth0Id.Equals(auth0UserId));

            if (auth0 == null)
            {
                filter.PageNumber = 1;
                filter.PageSize = 3;
            }
            
           

            if (FiltersNotApplied(filter))
            {
                var queryWithoutFilters = _context.Paintings
                    .Where(l => l.PaintingStatus.Equals("For Sale"));
                
                   var pagedList = PagedList<Painting>.ToPagedList(queryWithoutFilters, filter.PageNumber,
                       filter.PageSize);
               pagedList.TotalPages = 1;
               return pagedList;
            }

            IQueryable<Painting>  paintingQuery = _context.Paintings
                .Where(painting => painting.PaintingStatus.Equals("For Sale"))
                .Where(painting => filter.PaintingYear == null
                        ? true
                        : painting.PaintedYear.Equals(filter.PaintingYear))
                .Where(painting => filter.PaintingDescription == null 
                    ? true
                    : painting.PaintingDescription.ToLower().StartsWith(filter.PaintingDescription.ToLower())
                      | painting.PaintingName.ToLower().StartsWith(filter.PaintingDescription.ToLower()))
                .Where(l => (filter.RateStart == null || filter.RateEnd == null)
                    ? true
                    : l.PaintingPrice >= filter.RateStart && l.PaintingPrice <= filter.RateEnd
                );

            IQueryable<Painting> filteredPainting = _context.Paintings.Where(l => l.PaintingStatus.Equals("For Sale"));
            return new PagedList<Painting>(paintingQuery.ToList(),  filteredPainting.Count(),
                filter.PageNumber, filter.PageSize);
         
    }

    private bool FiltersNotApplied(FindPaintingFilter paintingFilter)
    {
        return paintingFilter.PaintingDescription == null  && paintingFilter.RateStart == null &&
               paintingFilter.RateEnd == null &&         paintingFilter.PaintingYear == null
            ;
    }
}