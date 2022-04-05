﻿using account_service.Controllers.SearchController;
using account_service.ValueObjects;

namespace account_service.Repository.SearchRepo;

public interface ISearchRepository
{
    PainterSearchResult FilterRegisterPainterForHomePage(PainterSearchField painter);
}