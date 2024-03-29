﻿using account_service.Controllers.Helper;
using account_service.Controllers.SearchController;
using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Repository.SearchRepo;

public interface ISearchRepository
{
    SearchResult FilterRegisterPainterForHomePage(PainterSearchField painter);
    PagedList<Painting> FilterPainting(FindPaintingFilter filter, string auth0UserId);
}