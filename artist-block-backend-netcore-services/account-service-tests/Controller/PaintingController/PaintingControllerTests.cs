using System;
using System.Collections.Generic;
using account_service.Controllers.PaintingController;
using account_service.CustomException;
using account_service.DTO.Painting;
using account_service.Models;
using account_service.Profile.PaintingProfile;
using account_service.Repository.PaintingRepo;
using account_service.Service.PaintingService;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.PaintingController;

public class PaintingController
{
    // create an automapper instance
    private static readonly List<Profile> Profiles = new() { new PaintingProfile() };
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(Profiles)).CreateMapper();

    // Stub for create painting service
    private readonly Mock<IPaintingService> _paintingServiceStub = new() ;

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

        _paintingServiceStub.Setup(service => service.CreatePainting(It.IsAny<Painting>(), It.IsAny<Guid>()))
                .Returns( stubReturnedPainting );

        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);
        
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

        _paintingServiceStub.Setup(service => service.CreatePainting(It.IsAny<Painting>(), It.IsAny<Guid>()))
            .Throws( new PainterDoesNotExistException("referenced is not present in the database") );

        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);
        
        // Act

        var returnedValue = controller.CreatePainting(_providedCreatePaintingDto, Guid.NewGuid()); // painterId not used in unit test

        // Assert
        returnedValue.Should().BeOfType<ObjectResult>();
    }


    [Fact]
    public async void UploadImage_ValidImageAndPaintingId_ReturnsOk()
    {
        // Arrange
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        var paintingId = Guid.NewGuid();
        Mock<IFormFile> imageMock = new();

        _paintingServiceStub.Setup(service => service.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>() , It.IsAny<Guid>()));

        // Act
        var value = await controller.UploadImage(imageMock.Object , paintingId);

        // Assert
        value.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async void UploadImage_ValidImageAndNotExistingPaintingId_ReturnsPaintingDoesNotExist()
    {
        // Arrange
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        var paintingId = Guid.NewGuid();
        Mock<IFormFile> imageMock = new();

        _paintingServiceStub.Setup(service => service.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>() , It.IsAny<Guid>()))
            .Throws( new PaintingDoesNotExist() );

        // Act
        var value = await controller.UploadImage(imageMock.Object , paintingId);

        // Assert
        value.Should().BeOfType<ObjectResult>();
    }

    [Fact]
    public void GetNRandomPaintingsForSale_Number_ReturnsOkResult()
    {
        // Arrange
        var numberOfRandom = 3;
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);

        _paintingServiceStub.Setup(service => service.GetNRandomPaintingsForSale(It.IsAny<int>()))
            .Returns(new List<Painting>());

        // Act
        var value = controller.getNRandomPaintingsForSale(numberOfRandom);

        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
    }

    private static readonly Painting _randomPaintingOne = new Painting()
    {
        PaintedYear = Guid.NewGuid().ToString(),
        PainterId = Guid.NewGuid(),
        PaintingDescription = Guid.NewGuid().ToString(),
        PaintingId = Guid.NewGuid(),
        PaintingName = Guid.NewGuid().ToString(),
        PaintingPrice = 1000,
        PaintingStatus = Guid.NewGuid().ToString(),
        PaintingUrl = Guid.NewGuid().ToString(),
        RegisteredUserId = Guid.NewGuid(),
        BuyTimeStamp = DateTime.Now,
        BoughtTimeStamp = DateTime.Now,
    };
        
    private static readonly Painting _randomPaintingTwo = new Painting()
    {
        PaintedYear = Guid.NewGuid().ToString(),
        PainterId = Guid.NewGuid(),
        PaintingDescription = Guid.NewGuid().ToString(),
        PaintingId = Guid.NewGuid(),
        PaintingName = Guid.NewGuid().ToString(),
        PaintingPrice = 1000,
        PaintingStatus = Guid.NewGuid().ToString(),
        PaintingUrl = Guid.NewGuid().ToString(),
        RegisteredUserId = Guid.NewGuid(),
        BuyTimeStamp = DateTime.Now,
        BoughtTimeStamp = DateTime.Now,
    };
    
    private readonly List<Painting> _expectedPaintingList = new List<Painting>() {  _randomPaintingOne , _randomPaintingTwo }; 

    [Fact]
    public void GetPaintingsForPainter_ValidPainterId_ReturnOkWithReadPaintingDtos()
    {
        // Arrange
        _paintingServiceStub.Setup(service => service.GetPaintingsForPainter(It.IsAny<Guid>()))
            .Returns(_expectedPaintingList);
        
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);
        // Act
        var value = controller.GetPaintingsForPainter(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
        var actualValue = (value.Result as OkObjectResult).Value;
        actualValue.Should().BeEquivalentTo(_mapper.Map<IEnumerable<ReadPaintingDto>>(_expectedPaintingList));
    }

    [Fact]
    public void GetPaintingsForPainter_InValidPainterId_ReturnsNotFound()
    {
        // Arrange
        _paintingServiceStub.Setup(service => service.GetPaintingsForPainter(It.IsAny<Guid>()))
            .Throws(new ContentNotFoundById("could find the painter by painterId"));
        
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);
        // Act
        var value = controller.GetPaintingsForPainter(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public void GetPaintingByPaintingId_ValidPaintingId_ReturnOkWithReadPaintingDto()
    {
        // Arrange
        _paintingServiceStub.Setup(service => service.GetPaintingByPaintingId(It.IsAny<Guid>()))
            .Returns(_randomPaintingOne);
        
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);
        // Act
        var value = controller.GetPaintingByPaintingId(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
        var actualValue = (value.Result as OkObjectResult).Value;
        actualValue.Should().BeEquivalentTo(_mapper.Map<ReadPaintingDto>(_randomPaintingOne));
    }
    
    [Fact]
    public void GetPaintingByPaintingId_InvalidPaintingId_ReturnNotFound()
    {
        // Arrange
        _paintingServiceStub.Setup(service => service.GetPaintingByPaintingId(It.IsAny<Guid>()))
            .Throws(new ContentNotFoundById("painting not found by painting Id"));
        
        var controller = new CreatePaintingController(_mapper, _paintingServiceStub.Object);
        // Act
        var value = controller.GetPaintingByPaintingId(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<NotFoundObjectResult>();
    }
    
}