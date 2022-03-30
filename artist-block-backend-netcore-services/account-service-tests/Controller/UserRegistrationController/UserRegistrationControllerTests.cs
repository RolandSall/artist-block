using System;
using System.Security.Claims;
using account_service.Controllers.Auth0TestController;
using account_service.Profile.ClientProfile;
using account_service.Repository.RegistrationRepo;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

using account_service.Controllers.UserRegistrationController;
using account_service.DTO.Registration;
using account_service.Models;
using account_service.Service.RegistrationService;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace account_service_tests.Controller.UserRegistrationController;

public class UserRegistrationControllerTests
{
    //private readonly Mock<IRegistrationRepo> _repositoryStub = new();
    // create an automapper instance for unit tests
    private readonly IMapper _mapperClient = new MapperConfiguration(mc => mc.AddProfile(new ClientProfile())).CreateMapper();
    private readonly Mock<IRegistrationService> _registrationServiceStub = new() ;
    private static readonly  DateTime _dateTime = DateTime.Now;
    private readonly CreateClientDto _providedCreateDto = new CreateClientDto()
    {
        Email = "user@example.com",
        Nationality = "Nationality",
        Title = "Title",
        BirthDate = _dateTime,
        FirstName = "FirstName",
        LastName = "LastName",
        PhoneNumber = "1111",
    };
    
    private readonly ReadClientDto _expectedReadDto = new ReadClientDto()
    {
        RegisteredUserId = Guid.NewGuid(),
        Email = "user@example.com",
        Nationality = "Nationality",
        Title = "Title",
        BirthDate = _dateTime,
        FirstName = "FirstName",
        LastName = "LastName",
        PhoneNumber = "1111",
    };

    //TODO: refactoring needed 
    [Fact]
    public void RegisterClient_NewCreateClientDto_ReturnsOkObject()
    {
        // Arrange

        // setup the stub
        var returnedValueRegisteredUserStub = _mapperClient.Map<RegisteredUser>(_providedCreateDto);
        returnedValueRegisteredUserStub.RegisteredUserId = _expectedReadDto.RegisteredUserId;
        
        _registrationServiceStub.Setup(
            service => service.RegisterClient( It.IsAny<RegisteredUser>() , It.IsAny<string>()))
            .Returns( returnedValueRegisteredUserStub );
        
        // Create Mock for User 
        var identity = new ClaimsIdentity();
        identity.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "auth0|123123123123"),
            new Claim(ClaimTypes.Name, "Roland Salloum"),
        });
        var user = new ClaimsPrincipal(identity);
        
        // Setup the controller with out Mocked User
        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        var controller = new account_service.Controllers.UserRegistrationController.UserRegistrationController(_mapperClient,_registrationServiceStub.Object)
        {
            ControllerContext = controllerContext,
        };
        
        // Act
        var returnedValue = controller.RegisterClient(_providedCreateDto);
        var value = ( ReadClientDto ) (returnedValue as OkObjectResult).Value;

        // Assert
        value.Should().BeEquivalentTo(_expectedReadDto , opt => opt.ComparingByMembers<ReadClientDto>());
    }

    [Fact]
    public void RegisterClient_AlreadyRegisteredClientDto_ThrowsUserAlreadyRegisteredException()
    {
        // Arrange

        // setup the stub, now it throws an exception
        _registrationServiceStub.Setup(
                service => service.RegisterClient( It.IsAny<RegisteredUser>() , It.IsAny<string>()))
            .Throws(new ClientAlreadyExistException("User Already Registered"));
        
        // Create Mock for User 
        var identity = new ClaimsIdentity();
        identity.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "auth0|123123123123"),
            new Claim(ClaimTypes.Name, "Roland Salloum"),
        });
        var user = new ClaimsPrincipal(identity);
        
        // Setup the controller with out Mocked User
        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        var controller = new account_service.Controllers.UserRegistrationController.UserRegistrationController(_mapperClient,_registrationServiceStub.Object)
        {
            ControllerContext = controllerContext,
        };
        
        // Act
        var returnedValue = controller.RegisterClient(_providedCreateDto);
        
        // Assert
        returnedValue.Should().BeOfType<ConflictObjectResult>();
    }
}