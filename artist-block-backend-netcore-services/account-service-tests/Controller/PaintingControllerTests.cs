using System;
using System.Collections.Generic;
using account_service.Controllers.PaintingController;
using account_service.DTO.Painting;
using account_service.Models;
using account_service.Profile.Painting;
using account_service.Repository.PaintingRepo;
using account_service.Service.PaintingService;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller;

public class PaintingController
{
    // create an automapper instance
    private static readonly List<Profile> profiles = new() { new PaintingProfile() };
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(profiles)).CreateMapper();

    // Mock for create painting service
    private readonly Mock<IPaintingService> _createPaintingServiceStub = new() ;

    private readonly CreatePaintingDto _providedCreatePaintingDto = new()
    {
        PaintedYear = "1999",
        PaintingDescription = "very beautiful",
        PaintingName = "tres belle",
        PaintingPrice = 2000,
        PaintingStatus = "For Sale",
    };

    private readonly ReadPaintingDto _expectedReadPaintingDto = new()
    {
        PaintingId = Guid.NewGuid(),
        PainterId = Guid.NewGuid(),
        PaintedYear = "1999",
        PaintingDescription = "very beautiful",
        PaintingName = "tres belle",
        PaintingPrice = 2000,
        PaintingStatus = "For Sale",
    };
    
    [Fact]
    public void CreatePainting_ValidPaintingDtoWithExistingPainter_ReturnsOkReadPaintingDto()
    {
        // Arrange

        var stubReturnedPainting = _mapper.Map<Painting>(_providedCreatePaintingDto);
        // setup ids
        stubReturnedPainting.PaintingId = _expectedReadPaintingDto.PaintingId;
        stubReturnedPainting.PainterId = _expectedReadPaintingDto.PainterId;

        _createPaintingServiceStub.Setup(service => service.CreatePainting(It.IsAny<Painting>(), It.IsAny<Guid>()))
                .Returns( stubReturnedPainting );

        var controller = new CreatePaintingController(_mapper, _createPaintingServiceStub.Object);
        
        // Act

        var returnedValue = controller.CreatePainting(_providedCreatePaintingDto, Guid.NewGuid()); // painterId not used in unit test
        var actualValue = (returnedValue as OkObjectResult).Value;

        // Assert
        actualValue.Should().BeEquivalentTo(_expectedReadPaintingDto , opt => opt.ComparingByMembers<ReadPaintingDto>());
    }

    [Fact]
    public void CreatePainting_CreatePaintingDtoWithNoPainter_ReturnsProblem()
    {
        // Arrange

        var stubReturnedPainting = _mapper.Map<Painting>(_providedCreatePaintingDto);
        // setup ids
        stubReturnedPainting.PaintingId = _expectedReadPaintingDto.PaintingId;
        stubReturnedPainting.PainterId = _expectedReadPaintingDto.PainterId;

        _createPaintingServiceStub.Setup(service => service.CreatePainting(It.IsAny<Painting>(), It.IsAny<Guid>()))
            .Throws( new PainterDoesNotExistException("referenced is not present in the database") );

        var controller = new CreatePaintingController(_mapper, _createPaintingServiceStub.Object);
        
        // Act

        var returnedValue = controller.CreatePainting(_providedCreatePaintingDto, Guid.NewGuid()); // painterId not used in unit test

        // Assert
        returnedValue.Should().BeOfType<ObjectResult>();
    }

}