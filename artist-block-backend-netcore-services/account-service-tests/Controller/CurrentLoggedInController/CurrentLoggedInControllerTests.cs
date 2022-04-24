using System;
using System.Collections.Generic;
using account_service.CustomException;
using account_service.DTO.Current;
using account_service.DTO.Registration;
using account_service.Models;
using account_service.Profile;
using account_service.Profile.PainterProfile;
using account_service.Service.CurrentLoggedInService;
using account_service.ValueObjects;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.CurrentLoggedInController;

public class CurrentLoggedInControllerTests
{
    private static readonly List<Profile> Profiles = new() {new PainterProfile(), new CurrentProfile()};
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(Profiles)).CreateMapper();
    private readonly Mock<ICurrentLoggedInService> _currentLoggedInService = new();

    private readonly Painter _expectedPainter = new()
    {
        Bio = Guid.NewGuid().ToString(),Location = Guid.NewGuid().ToString(),PainterId = Guid.NewGuid(),
        RegisteredUserId = Guid.NewGuid(),YearsOfExperience = Guid.NewGuid().ToString(),
    };

    private readonly CurrentUser _expectedCurrentUser = new()
    {
        Role = Guid.NewGuid().ToString(),
        RegisteredUser = new RegisteredUser()
        {
            Email = Guid.NewGuid().ToString(),Image = Guid.NewGuid().ToString(),
            Nationality = Guid.NewGuid().ToString(),Title = Guid.NewGuid().ToString(),BirthDate = DateTime.Now,FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),PhoneNumber = Guid.NewGuid().ToString(),RegisteredUserId = Guid.NewGuid(),
            Painter = null,PaintingsBought = null,ClaimedGanImages = null
        }
    };


        [Fact]
    public void GetCurrentLoggedInPainterInfo_Void_ReturnsOkReadPainterDto()
    {
        // Arrange
        _currentLoggedInService.Setup(service => service.GetCurrentLoggedInPainterInfo(It.IsAny<string>()))
            .Returns(_expectedPainter);

        var controller = new account_service.Controllers.CurrentLoggedInController.CurrentLoggedInController(_currentLoggedInService.Object,_mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act

        var value = controller.GetCurrentLoggedInPainterInfo();

        // Assert
        var containedValue = (ReadPainterDto)(value as OkObjectResult).Value;
        containedValue.Should().BeEquivalentTo(_mapper.Map<ReadPainterDto>(_expectedPainter));
    }

    [Fact]
    public void GetCurrentLoggedInPainterInfo_UserNotFound_ThrowsUserNotFoundException()
    {
        // Arrange
        _currentLoggedInService.Setup(service => service.GetCurrentLoggedInPainterInfo(It.IsAny<string>()))
            .Throws(new UserNotFoundException("user not found!"));

        var controller = new account_service.Controllers.CurrentLoggedInController.CurrentLoggedInController(_currentLoggedInService.Object,_mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act

        var value = controller.GetCurrentLoggedInPainterInfo();

        // Assert
        value.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public void GetCurrentLoggedInPainterInfo_GenericProblemOccured_ThrowsGenericException()
    {
        // Arrange
        _currentLoggedInService.Setup(service => service.GetCurrentLoggedInPainterInfo(It.IsAny<string>()))
            .Throws(new Exception("some generic problem occured!"));

        var controller = new account_service.Controllers.CurrentLoggedInController.CurrentLoggedInController(_currentLoggedInService.Object,_mapper)
        {
            ControllerContext = ControllerContextHelper.getStubControllerContext(),
        };

        // Act

        var value = controller.GetCurrentLoggedInPainterInfo();

        // Assert
        value.Should().BeOfType<ObjectResult>();
    }

    // [Fact]
    // public void GetCurrentLoggedInUser_Void_ReturnsOkWithReadCurrentUserDto()
    // {
    //     // Arrange
    //     _currentLoggedInService.Setup(service => service.GetCurrentLoggedInUser(It.IsAny<string>()))
    //         .Returns(_expectedCurrentUser);
    //     
    //     var controller = new account_service.Controllers.CurrentLoggedInController.CurrentLoggedInController(_currentLoggedInService.Object,_mapper)
    //     {
    //         ControllerContext = ControllerContextHelper.getStubControllerContext(),
    //     };
    //
    //     // Act
    //     var value = controller.GetCurrentLoggedInUser();
    //     
    //     // Assert
    //     var containedValue = (ReadCurrentLoggedInUserDto)(value as OkObjectResult).Value;
    //     containedValue.Should()
    //         .BeEquivalentTo(_mapper.Map<CurrentUser, ReadCurrentLoggedInUserDto>(_expectedCurrentUser));
    // }
    
    
    
}