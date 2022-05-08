using System;
using System.Collections.Generic;
using System.Security.Claims;
using account_service.Controllers.UserRegistrationController;
using account_service.CustomException;
using account_service.DTO.PainterSpecialityDto;
using account_service.DTO.Registration;
using account_service.Models;
using account_service.Profile.ClientProfile;
using account_service.Profile.PainterProfile;
using account_service.Repository.RegistrationRepo;
using account_service.Service.RegistrationService;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Xunit;

namespace account_service_tests.Controller.UserRegistrationControllerTests;

public class UserRegistrationControllerTests
{
    // create an automapper instance for unit tests
    private static readonly List<Profile> profiles = new() { new ClientProfile(), new PainterProfile() };
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(profiles)).CreateMapper();
        
    private readonly Mock<IRegistrationService> _registrationServiceStub = new() ;
    private static readonly  DateTime _dateTime = DateTime.Now;
    
    private static readonly CreateClientDto _providedCreateClientDto = new ()
    {
        Email = "user@example.com",
        Nationality = "Nationality",
        Title = "Title",
        BirthDate = _dateTime,
        FirstName = "FirstName",
        LastName = "LastName",
        PhoneNumber = "1111",
    };
    
    private readonly CreatePainterDto _providedCreatePainterDto = new ()
    {
        CreateClientDto = _providedCreateClientDto,
        Bio = "Very experienced at painting stuff",
        Location = "Congo",
        YearsOfExperience = "23+",
        AddPainterSpecialityDtos = new List<AddPainterSpecialityDto>(),
    };
    
    private static readonly ReadClientDto _expectedClientReadDto = new ()
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

    private readonly ReadPainterDto _expectedPainterReadDto = new ()
    {
        PainterId = Guid.NewGuid(),
        Bio = "Very experienced at painting stuff",
        Location = "Congo",
        YearsOfExperience = "23+",
        readClientDto = _expectedClientReadDto,
        PainterSpecialityDtos = new List<ReadPainterSpecialityDto>(),
    };
    
    // HELPERS *********
    
    private ControllerContext getStubControllerContext()
    {
        // Create Stub for User
        var identity = new ClaimsIdentity();
        identity.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "auth0|123123123123"),
            new Claim(ClaimTypes.Name, "Roland Salloum"),
        });
        var user = new ClaimsPrincipal(identity);
        
        // Setup the controller with out Mocked User
        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };

        return controllerContext;
    }
    
    // TESTS *************
    
    
    // REGISTER CLIENT ENDPOINT TESTS ************

    [Fact]
    public void RegisterClient_NewCreateClientDto_ReturnsOkObject()
    {
        // Arrange

        // setup the stub
        var returnedValueRegisteredUserStub = _mapper.Map<RegisteredUser>(_providedCreateClientDto);
        returnedValueRegisteredUserStub.RegisteredUserId = _expectedClientReadDto.RegisteredUserId;
        
        _registrationServiceStub.Setup(
            service => service.RegisterClient( It.IsAny<RegisteredUser>() , It.IsAny<string>()))
            .Returns( returnedValueRegisteredUserStub );
        
        
        var controller = new UserRegistrationController(_mapper,_registrationServiceStub.Object)
        {
            ControllerContext = getStubControllerContext(),
        };
        
        // Act
        var returnedValue = controller.RegisterClient(_providedCreateClientDto);
        var value = ( ReadClientDto ) (returnedValue as OkObjectResult).Value;

        // Assert
        value.Should().BeEquivalentTo(_expectedClientReadDto , opt => opt.ComparingByMembers<ReadClientDto>());
    }

    [Fact]
    public void RegisterClient_AlreadyRegisteredClientDto_ThrowsUserAlreadyRegisteredException()
    {
        // Arrange

        // setup the stub, now it throws an exception
        _registrationServiceStub.Setup(
                service => service.RegisterClient( It.IsAny<RegisteredUser>() , It.IsAny<string>()))
            .Throws(new ClientAlreadyExistException("User Already Registered"));
        
        var controller = new UserRegistrationController(_mapper,_registrationServiceStub.Object)
        {
            ControllerContext = getStubControllerContext(),
        };
        
        // Act
        var returnedValue = controller.RegisterClient(_providedCreateClientDto);
        
        // Assert
        returnedValue.Should().BeOfType<ConflictObjectResult>();
    }

    [Fact]
    public void RegisterClient_ClientDto_ThrowGenericException()
    {
        // Arrange

        // setup the stub, now it throws a Generic exception type 
        _registrationServiceStub.Setup(
                service => service.RegisterClient( It.IsAny<RegisteredUser>() , It.IsAny<string>()))
            .Throws(new Exception("Some Problem Occured"));
        
        var controller = new UserRegistrationController(_mapper,_registrationServiceStub.Object)
        {
            ControllerContext = getStubControllerContext(),
        };
        
        // Act
        var returnedValue = controller.RegisterClient(_providedCreateClientDto);
        
        // Assert
        returnedValue.Should().BeOfType<ObjectResult>();
    }
    
    // REGISTER PAINTER ENDPOINT TESTS ************
    
    /*
    [Fact]
    public void RegisterPainter_NewCreatePainterDto_ReturnsOkObject()
    {
        // Arrange
        
        // setup the stub
        var returnedValueRegisteredPainterStub = _mapper.Map<Painter>(_providedCreatePainterDto);
        // setup ids
        returnedValueRegisteredPainterStub.PainterId = _expectedPainterReadDto.PainterId;
        returnedValueRegisteredPainterStub.RegisteredUser.RegisteredUserId = _expectedClientReadDto.RegisteredUserId;
        
        _registrationServiceStub.Setup(
                service => service.RegisterPainter( It.IsAny<Painter>() , It.IsAny<string>()))
            .Returns( returnedValueRegisteredPainterStub );
        
        var controller = new UserRegistrationController(_mapper,_registrationServiceStub.Object)
        {
            ControllerContext = getStubControllerContext(),
        };

        // Act

        var returnedReadPainterDto = controller.RegisterPainter( _providedCreatePainterDto ); 
        var value = ( ReadPainterDto ) (returnedReadPainterDto as OkObjectResult).Value;

        // Assert
        value.Should().BeEquivalentTo(_expectedPainterReadDto, opt => opt.ComparingByMembers<ReadPainterDto>());
    }*/
    
    [Fact]
    public void RegisterPainter_AlreadyRegisteredPainterDto_ReturnsAlreadyRegisteredException()
    {
        // Arrange
        _registrationServiceStub.Setup(
                service => service.RegisterPainter( It.IsAny<Painter>() , It.IsAny<string>()))
            .Throws( new ClientAlreadyExistException("User Already Registered") );
        
        var controller = new UserRegistrationController(_mapper,_registrationServiceStub.Object)
        {
            ControllerContext = getStubControllerContext(),
        };

        // Act

        var returnedValue = controller.RegisterPainter( _providedCreatePainterDto );

        // Assert
        returnedValue.Should().BeOfType<ConflictObjectResult>();
    }
    
    [Fact]
    public void RegisterPainter_CreatePainterDto_ReturnsGenericException()
    {
        // Arrange
        _registrationServiceStub.Setup(
                service => service.RegisterPainter( It.IsAny<Painter>() , It.IsAny<string>()))
            .Throws( new Exception("Some Problem Occured") );
        
        var controller = new UserRegistrationController(_mapper,_registrationServiceStub.Object)
        {
            ControllerContext = getStubControllerContext(),
        };

        // Act

        var returnedValue = controller.RegisterPainter( _providedCreatePainterDto );

        // Assert
        returnedValue.Should().BeOfType<ObjectResult>();
    }

    private static readonly Painter _expectedPainter = new Painter()
    {
        Bio = Guid.NewGuid().ToString(),
        Location = Guid.NewGuid().ToString(),
        YearsOfExperience = Guid.NewGuid().ToString(),
        PainterId = Guid.NewGuid(),
        RegisteredUserId = Guid.NewGuid(),
    };

    [Fact]
    public void GetPainterById_ValidPainterId_ReturnsOkWithReadPainterDto()
    {
        // Arrange
        _registrationServiceStub.Setup(
                service => service.GetPainterById( It.IsAny<Guid>()))
            .Returns(_expectedPainter);

        var controller = new UserRegistrationController(_mapper, _registrationServiceStub.Object);
        // Act

        var returnedValue = controller.GetPainterById( Guid.NewGuid() );

        // Assert
        returnedValue.Result.Should().BeOfType<OkObjectResult>();
        var containedValue = (ReadPainterDto)(returnedValue.Result as OkObjectResult).Value;
        containedValue.Should().BeEquivalentTo(_mapper.Map<ReadPainterDto>(_expectedPainter));
    }
    
    [Fact]
    public void GetPainterById_InvalidPainterId_ReturnsNotFound()
    {
        // Arrange
        _registrationServiceStub.Setup(
                service => service.GetPainterById( It.IsAny<Guid>()))
            .Throws(new ContentNotFoundById("could not find painter with Id"));

        var controller = new UserRegistrationController(_mapper, _registrationServiceStub.Object);
        // Act

        var returnedValue = controller.GetPainterById( Guid.NewGuid() );

        // Assert
        returnedValue.Result.Should().BeOfType<NotFoundObjectResult>();
    }
}