using System;
using System.Collections.Generic;
using account_service.CustomException;
using account_service.DTO.Gan;
using account_service.Models;
using account_service.Service.GanService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.GanController;

public class GanControllerTests
{
    private readonly Mock<IGanService> _ganService = new();

    private readonly ClaimGanImageDto _providedDto = new()
    {
        Description = "awesome!"
    };

    private readonly ReadClaimGanImageDto _expected = new()
    {
        GanImageId = Guid.NewGuid(),
    };

    [Fact]
    public void ClaimImage_ValidClaimGanImageDto_OkWithReadClaimImageDto()
    {
        // Arrange

        _ganService.Setup(service => service.ClaimGanImage(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(_expected.GanImageId);

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.ClaimImage(_providedDto);
        var containedValue = (ReadClaimGanImageDto) ((value as OkObjectResult).Value);
        // Assert
        value.Should().BeOfType<OkObjectResult>();
        containedValue.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public void ClaimImage_SomeProblemOccurs_ReturnsProblem()
    {
        // Arrange
        _ganService.Setup(service => service.ClaimGanImage(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception("some generic problem occured!"));

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.ClaimImage(_providedDto);
        // Assert
        value.Should().BeOfType<ObjectResult>();
    }

    private readonly Mock<IFormFile> _mockImage = new ();
    
    
    [Fact]
    public void UploadImage_ValidImageAndPaintingId_ReturnsOkWithMessage()
    {
        // Arrange
        _ganService.Setup(service => service.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Guid>()));

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.UploadImage(_mockImage.Object, Guid.NewGuid());
        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public void UploadImage_InvalidGanPaintingId_ReturnsNotFound()
    {
        // Arrange
        _ganService.Setup(service => service.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Guid>()))
            .Throws(new GanGeneratedImageNotFoundException("not found") );

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.UploadImage(_mockImage.Object, Guid.NewGuid());
        // Assert
        value.Result.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public void UploadImage_SomeGenericProblemOccurs_ReturnsProblem()
    {
        // Arrange
        _ganService.Setup(service => service.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Guid>()))
            .Throws(new Exception("some generic problem occured!"));

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.UploadImage(_mockImage.Object, Guid.NewGuid());
        // Assert
        value.Result.Should().BeOfType<ObjectResult>();
    }
    
    
    
    
    
    
    
    
    
    
    
    


    private readonly List<GanGeneratedImage> _expectedGanGeneratedImages = new()
    {
        new () {Description = "description 0",ImageUrl = "URL 0",GanImageId = Guid.NewGuid(),RegisteredUserId = Guid.NewGuid(),},
        new () {Description = "description 1",ImageUrl = "URL 1",GanImageId = Guid.NewGuid(),RegisteredUserId = Guid.NewGuid(),},
        new () {Description = "description 2",ImageUrl = "URL 2",GanImageId = Guid.NewGuid(),RegisteredUserId = Guid.NewGuid(),},
    };


    [Fact]
    public void GetAllClaimedGanImagesForClient_Void_ReturnsOkWithGanGeneratedImages()
    {
        // Arrange
        _ganService.Setup(service => service.GetAllClaimedGanImagesForClient(It.IsAny<string>()))
            .Returns(_expectedGanGeneratedImages);

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.GetAllClaimedGanImagesForClient();
        // Assert
        value.Should().BeOfType<OkObjectResult>();
        var containedValue = (IEnumerable<GanGeneratedImage> )(value as OkObjectResult).Value;

        containedValue.Should().BeEquivalentTo(_expectedGanGeneratedImages);
    }

    [Fact]
    public void GetAllClaimedGanImagesForClient_SomeProblemOccurs_ReturnsProblem()
    {
        // Arrange
        _ganService.Setup(service => service.GetAllClaimedGanImagesForClient(It.IsAny<string>()))
            .Throws(new Exception("some generic problem occured!"));

        var controller = new account_service.Controllers.GanController.GanController(_ganService.Object)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };

        // Act
        var value = controller.GetAllClaimedGanImagesForClient();
        // Assert
        value.Should().BeOfType<ObjectResult>();
    }
}