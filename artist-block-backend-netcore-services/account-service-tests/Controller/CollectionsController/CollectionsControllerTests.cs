using System;
using System.Collections.Generic;
using account_service.Controllers.CollectionController;
using account_service.CustomException;
using account_service.Models;
using account_service.Profile.PaintingProfile;
using account_service.Service.CollectionService;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.CollectionsController;

public class CollectionsControllerTests
{
    private static readonly List<Profile> Profiles = new() {new PaintingProfile()};
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(Profiles)).CreateMapper();
    private readonly Mock<ICollectionService> _collectionService = new();

    private readonly List<Painting> _expectedPaintings = new List<Painting>()
    {  
        new(){PainterId = Guid.NewGuid(),PaintingId = Guid.NewGuid(),PaintingName = "hello beautiful 0"},
        new(){PainterId = Guid.NewGuid(),PaintingId = Guid.NewGuid(),PaintingName = "hello beautiful 1"},
        new(){PainterId = Guid.NewGuid(),PaintingId = Guid.NewGuid(),PaintingName = "hello beautiful 2"},
        new(){PainterId = Guid.NewGuid(),PaintingId = Guid.NewGuid(),PaintingName = "hello beautiful 3"},
    };

    [Fact]
    public void GetCurrentLoggedInUserPaintingCollection_Void_ReturnsOk()
    {
        // Arrange

        _collectionService.Setup(service => service.GetCurrentLoggedInUserPaintingCollection(It.IsAny<string>()))
            .Returns(_expectedPaintings);
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetCurrentLoggedInUserPaintingCollection();

        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
        var containedValue = (value.Result as OkObjectResult).Value;

        containedValue.Should().BeEquivalentTo(_expectedPaintings);
    }

    [Fact]
    public void GetCurrentLoggedInUserPaintingCollection_UserNotFound_ThrowsUserNotFoundException()
    {
        // Arrange

        _collectionService.Setup(service => service.GetCurrentLoggedInUserPaintingCollection(It.IsAny<string>()))
            .Throws(new UserNotFoundException("user is not found!"));
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetCurrentLoggedInUserPaintingCollection();

        // Assert
        value.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public void GetCurrentLoggedInUserPaintingCollection_SomeGenericProblemOccured_ThrowsGenericException()
    {
        // Arrange

        _collectionService.Setup(service => service.GetCurrentLoggedInUserPaintingCollection(It.IsAny<string>()))
            .Throws(new Exception("some generic problem occured"));
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetCurrentLoggedInUserPaintingCollection();

        // Assert
        value.Result.Should().BeOfType<ObjectResult>();
    }


    [Fact]
    public void GetCurrentPainterOwnedPaintings_Void_ReturnsOkWithPainterOwnedPaintings()
    {
        // Arrange

        _collectionService.Setup(service => service.GetCurrentPainterOwnedPaintings(It.IsAny<string>()))
            .Returns(_expectedPaintings);
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetCurrentPainterOwnedPaintings();

        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
        var containedValue = (value.Result as OkObjectResult).Value;

        containedValue.Should().BeEquivalentTo(_expectedPaintings);
    }
    
    [Fact]
    public void GetCurrentPainterOwnedPaintings_UserNotFound_ThrowsUserNotFoundException()
    {
        // Arrange

        _collectionService.Setup(service => service.GetCurrentPainterOwnedPaintings(It.IsAny<string>()))
            .Throws(new UserNotFoundException("user is not found"));
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetCurrentPainterOwnedPaintings();

        // Assert
        value.Result.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public void GetCurrentPainterOwnedPaintings_GenericProblemOccured_ThrowsGenericException()
    {
        // Arrange

        _collectionService.Setup(service => service.GetCurrentPainterOwnedPaintings(It.IsAny<string>()))
            .Throws(new Exception("some generic problem occured"));
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetCurrentPainterOwnedPaintings();

        // Assert
        value.Result.Should().BeOfType<ObjectResult>();
    }
    
    
    [Fact]
    public void GetPaintingCollectionByUserId_Void_ReturnsOkWithPainterOwnedPaintings()
    {
        // Arrange

        _collectionService.Setup(service => service.GetPaintingCollectionByUserId(It.IsAny<Guid>()))
            .Returns(_expectedPaintings);
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetPaintingCollectionByUserId(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<OkObjectResult>();
        var containedValue = (value.Result as OkObjectResult).Value;

        containedValue.Should().BeEquivalentTo(_expectedPaintings);
    }
    
    [Fact]
    public void GetPaintingCollectionByUserId_UserNotFound_ThrowsUserNotFoundException()
    {
        // Arrange

        _collectionService.Setup(service => service.GetPaintingCollectionByUserId(It.IsAny<Guid>()))
            .Throws(new UserNotFoundException("user is not found"));
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetPaintingCollectionByUserId(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public void GetPaintingCollectionByUserId_GenericProblemOccured_ThrowsGenericException()
    {
        // Arrange

        _collectionService.Setup(service => service.GetPaintingCollectionByUserId(It.IsAny<Guid>()))
            .Throws(new Exception("some generic problem occured"));
        
        var controller = new CollectionController(_collectionService.Object, _mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act
        var value = controller.GetPaintingCollectionByUserId(Guid.NewGuid());

        // Assert
        value.Result.Should().BeOfType<ObjectResult>();
    }
    
}