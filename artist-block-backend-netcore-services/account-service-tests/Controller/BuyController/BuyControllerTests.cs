using System;
using account_service.Controllers.BuyController;
using account_service.CustomException;
using account_service.Repository.PaintingRepo;
using account_service.Service.BuyController;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.BuyController;

public class BuyControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IBuyService> _buyServiceStub = new() ;
    
    [Fact]
    public void BuyPainting_ValidPaintingId_ReturnsOk()
    {
        // Arrange

        var paintingId = Guid.NewGuid();
        var controller = new account_service.Controllers.BuyController.BuyController( _mapperMock.Object , _buyServiceStub.Object )
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        _buyServiceStub.Setup(service => service.BuyPainting(It.IsAny<Guid>(), It.IsAny<string>()));

        // Act
        var value = controller.BuyPainting( paintingId );

        // Assert

        value.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void BuyPainting_PaintingIdButPainterDoesNotExist_ThrowsPainterDoesNotExistException()
    {
        // Arrange

        var paintingId = Guid.NewGuid();
        var controller = new account_service.Controllers.BuyController.BuyController( _mapperMock.Object , _buyServiceStub.Object )
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        _buyServiceStub.Setup(service => service.BuyPainting(It.IsAny<Guid>(), It.IsAny<string>()))
            .Throws( new PainterDoesNotExistException("painter does not exist"));

        // Act
        var value = controller.BuyPainting( paintingId );

        // Assert

        value.Should().BeOfType<ObjectResult>();
    }
    
    [Fact]
    public void BuyPainting_PaintingIdButPaintingDoesNotExist_ThrowsPaintingDoesNotExistException()
    {
        // Arrange

        var paintingId = Guid.NewGuid();
        var controller = new account_service.Controllers.BuyController.BuyController( _mapperMock.Object , _buyServiceStub.Object )
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        _buyServiceStub.Setup(service => service.BuyPainting(It.IsAny<Guid>(), It.IsAny<string>()))
            .Throws( new PaintingDoesNotExist("painting does not exist"));

        // Act
        var value = controller.BuyPainting( paintingId );

        // Assert

        value.Should().BeOfType<NotFoundObjectResult>();
    }
    
    
    
    
    [Fact]
    public void SellPainting_ValidPaintingIdAndRequest_ReturnsOk()
    {
        // Arrange

        var paintingId = Guid.NewGuid();
        Mock<PaintingStatusRequest> request = new(); // Mock the request
        
        var controller = new account_service.Controllers.BuyController.BuyController( _mapperMock.Object , _buyServiceStub.Object )
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        _buyServiceStub.Setup(service => service.SellPainting(It.IsAny<Guid>(), It.IsAny<string>() , It.IsAny<PaintingStatusRequest>()));

        // Act
        var value = controller.SellPainting( paintingId , request.Object );

        // Assert

        value.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public void SellPainting_PaintingIdAndRequestButPainterDoesNotExist_ThrowsPainterDoesNotExistException()
    {
        // Arrange

        var paintingId = Guid.NewGuid();
        Mock<PaintingStatusRequest> request = new(); // Mock the request

        var controller = new account_service.Controllers.BuyController.BuyController( _mapperMock.Object , _buyServiceStub.Object )
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        _buyServiceStub.Setup(service => service.SellPainting(It.IsAny<Guid>(), It.IsAny<string>() , It.IsAny<PaintingStatusRequest>()))
            .Throws( new PainterDoesNotExistException("painter does not exist"));

        // Act
        var value = controller.SellPainting( paintingId , request.Object );

        // Assert

        value.Should().BeOfType<ObjectResult>();
    }
    
    [Fact]
    public void SellPainting_PaintingIdAndRequestButPaintingDoesNotExist_ThrowsPaintingDoesNotExistException()
    {
        // Arrange

        var paintingId = Guid.NewGuid();
        Mock<PaintingStatusRequest> request = new(); // Mock the request
        var controller = new account_service.Controllers.BuyController.BuyController( _mapperMock.Object , _buyServiceStub.Object )
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        _buyServiceStub.Setup(service => service.SellPainting(It.IsAny<Guid>(), It.IsAny<string>() , It.IsAny<PaintingStatusRequest>()))
            .Throws( new PaintingDoesNotExist("painting does not exist"));

        // Act
        var value = controller.SellPainting( paintingId , request.Object );

        // Assert

        value.Should().BeOfType<NotFoundObjectResult>();
    }
}