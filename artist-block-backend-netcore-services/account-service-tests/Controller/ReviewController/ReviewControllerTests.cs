using System;
using System.Collections.Generic;
using account_service.CustomException;
using account_service.DTO.PaintingReview;
using account_service.Models;
using account_service.Profile.PaintingReviewProfile;
using account_service.Service.ReviewService;
using AutoMapper;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.ReviewController;

public class ReviewControllerTests
{
    // create an automapper instance
    private static readonly List<Profile> Profiles = new() { new PaintingReviewProfile() };
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(Profiles)).CreateMapper();

    // Stub for create painting service
    private readonly Mock<IReviewService> _paintingServiceStub = new();

    private readonly CreatePaintingReviewDto _provided = new()
    {
        Comment = "Very nice OMG",
        LikeStatus = true,
    };

    private readonly PaintingReview _expected = new()
    {
        Comment = "Very nice OMG",
        LikeStatus = true,
        Timestamp = DateTime.Now,
        PaintingId = Guid.NewGuid(),
        PaintingReviewId = Guid.NewGuid(),
        RegisteredUserId = Guid.NewGuid(),
    };
    

    [Fact]
    public void CreatePaintingReview_ValidPaintingId_ReturnsOkWithReadDto()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.CreatePaintingReview(It.IsAny<PaintingReview>(),
            It.IsAny<Guid>(), It.IsAny<string>() )).Returns(_expected);
        
        var controller = new account_service.Controllers.ReviewController.ReviewController( _paintingServiceStub.Object,_mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };
        
        // Act
        var value = controller.CreatePaintingReview(_provided , Guid.NewGuid());
        var containedValue = (ReadPaintingReviewDto) ((value as OkObjectResult).Value);

        // Assert
        value.Should().BeOfType<OkObjectResult>();
        containedValue.Should().BeEquivalentTo(_mapper.Map<ReadPaintingReviewDto>(_expected));
    }


    [Fact]
    public void CreatePaintingReview_ReviewAlreadyExists_ReturnsProblem()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.CreatePaintingReview(It.IsAny<PaintingReview>(),
            It.IsAny<Guid>(), It.IsAny<string>() )).Throws(new PaintingReviewAlreadyPresentException());
        
        var controller = new account_service.Controllers.ReviewController.ReviewController( _paintingServiceStub.Object,_mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };
        
        // Act
        var value = controller.CreatePaintingReview(_provided , Guid.NewGuid());

        // Assert
        value.Should().BeOfType<ObjectResult>();
    }
    
    [Fact]
    public void CreatePaintingReview_InvalidPaintingId_ReturnsProblem()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.CreatePaintingReview(It.IsAny<PaintingReview>(),
            It.IsAny<Guid>(), It.IsAny<string>() )).Throws(new ContentNotFoundById());
        
        var controller = new account_service.Controllers.ReviewController.ReviewController( _paintingServiceStub.Object,_mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext()
        };
        
        // Act
        var value = controller.CreatePaintingReview(_provided , Guid.NewGuid());

        // Assert
        value.Should().BeOfType<ObjectResult>();
    }

    private static readonly List<PaintingReview> _expectedList = new()
    {
        new PaintingReview(){Comment = "whatever 1",Timestamp = DateTime.Now,LikeStatus = true,PaintingId = Guid.NewGuid(),
            PaintingReviewId = Guid.NewGuid(),RegisteredUserId = Guid.NewGuid()},
        
        new PaintingReview(){Comment = "whatever 2",Timestamp = DateTime.Now,LikeStatus = true,PaintingId = Guid.NewGuid(),
            PaintingReviewId = Guid.NewGuid(),RegisteredUserId = Guid.NewGuid()},
        
        new PaintingReview(){Comment = "whatever 3",Timestamp = DateTime.Now,LikeStatus = true,PaintingId = Guid.NewGuid(),
            PaintingReviewId = Guid.NewGuid(),RegisteredUserId = Guid.NewGuid()},
    };

    [Fact]
    public void GetPaintingReviews_ValidPaintingId_ReturnsOk()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.GetPaintingReviews(It.IsAny<Guid>()))
            .Returns(_expectedList);

        var controller =
            new account_service.Controllers.ReviewController.ReviewController(_paintingServiceStub.Object, _mapper);

        // Act
        var value = controller.GetPaintingReviews(Guid.NewGuid());
        var containedValue = (IEnumerable<ReadPaintingReviewDto>) ((value.Result as OkObjectResult).Value);

        // Assert
        containedValue.Should().BeEquivalentTo(_mapper.Map<IEnumerable<ReadPaintingReviewDto>>(_expectedList));
    }
    
    [Fact]
    public void GetPaintingReviews_InValidPaintingId_ReturnsProblem()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.GetPaintingReviews(It.IsAny<Guid>()))
            .Throws(new ContentNotFoundById());

        var controller =
            new account_service.Controllers.ReviewController.ReviewController(_paintingServiceStub.Object, _mapper);

        // Act
        var value = controller.GetPaintingReviews(Guid.NewGuid());
        // var containedValue = 

        // Assert
        value.Result.Should().BeOfType<ObjectResult>();
    }
    
    //TODO: add units test for GetPaintingReview

    [Fact]
    public void DeletePaintingReview_ValidReviewId_ReturnsNoContent()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.DeletePaintingReview(It.IsAny<Guid>()));

        var controller =
            new account_service.Controllers.ReviewController.ReviewController(_paintingServiceStub.Object, _mapper);

        // Act
        var value = controller.DeletePaintingReview(Guid.NewGuid());
        // var containedValue = 

        // Assert
        value.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public void DeletePaintingReview_InValidReviewId_ReturnsProblem()
    {
        // Arrange
        // stub for the review service 
        _paintingServiceStub.Setup(service => service.DeletePaintingReview(It.IsAny<Guid>()))
            .Throws(new ContentNotFoundById());

        var controller =
            new account_service.Controllers.ReviewController.ReviewController(_paintingServiceStub.Object, _mapper);

        // Act
        var value = controller.DeletePaintingReview(Guid.NewGuid());
        // var containedValue = 

        // Assert
        value.Should().BeOfType<ObjectResult>();
    }
}