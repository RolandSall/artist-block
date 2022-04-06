using System;
using System.Collections.Generic;
using account_service.Controllers.SearchController;
using account_service.Service.SearchService;
using account_service.ValueObjects;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.SearchController;

public class SearchControllerTests
{
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ISearchService> _searchService = new() ;

    // NOTE: both PainterList and PaintingList won't be populated at the same time in real scenarios
    private static readonly SearchResult SearchResult  = new ()
    {
        PainterList = new List<PainterSearchHeader> {new(){FirstName = "Roland" , LastName = "Salloum" , PainterId = Guid.NewGuid()}},
        PaintingList = new List<PaintingSearchHeader>{new(){PaintingId = Guid.NewGuid() , PaintingName = "hello" , PaintingYear = "1234"}},
    };
    
    // just for consistency mainly, only returns ok
    [Fact]
    public void FilterRegisterPainterForHomePage_PainterSearchField_ReturnOkWithSearchResult()
    {
        // arrange
        Mock<PainterSearchField> painterMock = new();
        _searchService.Setup(service => service.FilterRegisterPainterForHomePage(It.IsAny<PainterSearchField>()))
            .Returns( SearchResult );

        var controller =
            new account_service.Controllers.SearchController.SearchController(_searchService.Object, _mapper.Object);
        // act

        var value = controller.FilterRegisterPainterForHomePage( painterMock.Object );
        var containedValue = (SearchResult) ((value as OkObjectResult).Value);

        // assert 

        value.Should().BeOfType<OkObjectResult>();
        // make sure it returns the value that the service returns 
        containedValue.Should().BeEquivalentTo(SearchResult);
    }
}