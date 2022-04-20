using System;
using System.Collections.Generic;
using account_service.Controllers.SpecialityController;
using account_service.DTO.Speciality;
using account_service.Models;
using account_service.Profile.SpecialityProfile;
using account_service.Repository.RegistrationRepo;
using account_service.Service.SpecialityService;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.SpecialtyController;

public class SpecialtyControllerTests
{
    private static readonly List<Profile> Profiles = new() {new SpecialityProfile()};
    private readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfiles(Profiles)).CreateMapper();

    private readonly Mock<ISpecialityService> _specialtyService = new();

    private readonly CreateSpecialityDto _createDto = new()
    {
        SpecialityType = "just being good"
    };

    private readonly Speciality _expected = new()
    {
        SpecialityId = Guid.NewGuid(),
        SpecialityType = "just being good",
    };

    [Fact]
    public void AddSpecialty_ValidSpecialtyDto_ReturnsOkWithSpecialtyReadDto()
    {
        // Arrange
        _specialtyService.Setup(service => service.AddSpeciality(It.IsAny<Speciality>()))
            .Returns<Speciality>(x => x.SpecialityType.Equals(_expected.SpecialityType) ? _expected : null);
        // Act

        var controller = new SpecialityController(_specialtyService.Object, _mapper);

        // Assert
        var value = controller.AddSpeciality(_createDto);
        value.Should().BeOfType<OkObjectResult>();

        var containedValue = (ReadSpecialityDto) (value as OkObjectResult).Value;
        containedValue.Should().BeEquivalentTo(_mapper.Map<ReadSpecialityDto>(_expected));
    }


    [Fact]
    public void AddSpecialty_AlreadyCreatedSpecialtyDto_ThrowsSpecialtyAlreadyExistsException()
    {
        // Arrange
        _specialtyService.Setup(service => service.AddSpeciality(It.IsAny<Speciality>()))
            .Throws(new SpecialityAlreadyExistsException("this specialty already exists"));
        // Act

        var controller = new SpecialityController(_specialtyService.Object, _mapper);

        // Assert
        var value = controller.AddSpeciality(_createDto);
        value.Should().BeOfType<ConflictObjectResult>();
    }
    
    [Fact]
    public void AddSpecialty_GeneralErrorOccurs_ThrowsGenericException()
    {
        // Arrange
        _specialtyService.Setup(service => service.AddSpeciality(It.IsAny<Speciality>()))
            .Throws(new Exception("some generic issue occured"));
        // Act

        var controller = new SpecialityController(_specialtyService.Object, _mapper);

        // Assert
        var value = controller.AddSpeciality(_createDto);
        value.Should().BeOfType<ObjectResult>();
    }


    private readonly List<Speciality> _expectedSpecialties = new()
    {
        new Speciality(){SpecialityId = Guid.NewGuid(),SpecialityType = "just being good 1"},
        new Speciality(){SpecialityId = Guid.NewGuid(),SpecialityType = "just being good 2"},
        new Speciality(){SpecialityId = Guid.NewGuid(),SpecialityType = "just being good 3"},
    };

    
    [Fact]
    public void GetAllSpecialities_Void_ReturnsOkWithSpecialtyReadDtos()
    {
        // Arrange
        _specialtyService.Setup(service => service.GetAllSpecialities())
            .Returns(_expectedSpecialties);
        // Act

        var controller = new SpecialityController(_specialtyService.Object, _mapper);

        // Assert
        var value = controller.GetAllSpecialities();
        value.Should().BeOfType<OkObjectResult>();

        var containedValue = (IEnumerable<ReadSpecialityDto>) (value as OkObjectResult).Value;
        containedValue.Should().BeEquivalentTo(_mapper.Map<IEnumerable<ReadSpecialityDto>>(_expectedSpecialties));
    }
    
    
    [Fact]
    public void GetAllSpecialities_SomeGenericProblemOccurs_ReturnsProblem()
    {
        // Arrange
        _specialtyService.Setup(service => service.GetAllSpecialities())
            .Throws(new Exception("some generic problem occured"));
        // Act

        var controller = new SpecialityController(_specialtyService.Object, _mapper);

        // Assert
        var value = controller.GetAllSpecialities();
        value.Should().BeOfType<ObjectResult>();
    }
    
    
    
    
}
    