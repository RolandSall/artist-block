using System.Collections.Generic;
using account_service.Service.StatsService;
using account_service.ValueObjects;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.StatsController;

public class StatsControllerTests
{
    // // create an automapper instance

    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IStatsService> _statsService = new();

    private readonly List<IdAndValue> _expectedList = new()
    {
        new IdAndValue {Value = 2,Id = "Uzbekistan"},
        new IdAndValue {Value = 10,Id = "Pakistan"},
        new IdAndValue(){Value = 15,Id = "Kazakhstan"},
    };

    // used for both GetNumPaintersAndUsers and GetNumGanAndNormalPaintings tests
    private readonly List<IdAndValue> _expectedPaintersAndUsers= new List<IdAndValue>()
    {
        new() {Id = "Client", Value = 20 },
        new() {Id = "Painter", Value = 10},
    };
    
    private readonly NumPaintingsAndGan _expectedPaintingsAndGan = new() { numGans = 10 , numPaintings = 20};

    [Fact]
    public void GetNumPaintersByCountry_ReturnsOkWithEntries()
    {
        // Arrange
        _statsService.Setup(service => service.GetNumPaintersByCountry())
            .Returns(_expectedList);
        
        var controller = new account_service.Controllers.StatsController.StatsController( _mapper.Object , _statsService.Object );
        
        // Act
        var returned = controller.GetNumPaintersByCountry();
        var containedValue = (returned.Result as OkObjectResult).Value;
        // Assert 
    
        containedValue.Should().BeEquivalentTo(_expectedList);
    }

    [Fact]
    public void GetNumPaintersAndUsers_ReturnsOkWithPair()
    {
        // Arrange
        _statsService.Setup(service => service.GetNumPaintersAndUsers())
            .Returns(_expectedPaintersAndUsers);
        
        var controller = new account_service.Controllers.StatsController.StatsController( _mapper.Object , _statsService.Object );
        
        // Act
        var returned = controller.GetNumPaintersAndUsers();
        var containedValue = (returned.Result as OkObjectResult).Value;
        // Assert 
    
        containedValue.Should().BeEquivalentTo(_expectedPaintersAndUsers);
    }

    [Fact]
    public void GetNumGanAndNormalPaintings_ReturnsOkWithPair()
    {
        // Arrange
        _statsService.Setup(service => service.GetNumGanAndNormalPaintings())
            .Returns(_expectedPaintingsAndGan);
        
        var controller = new account_service.Controllers.StatsController.StatsController( _mapper.Object , _statsService.Object );
        
        // Act
        var returned = controller.GetNumGanAndNormalPaintings();
        var containedValue = (returned.Result as OkObjectResult).Value;
        // Assert 

        containedValue.Should().BeEquivalentTo(_expectedPaintingsAndGan);
    }


}