using System;
using account_service.Service.DeploymentService;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace account_service_tests.Controller.DeploymentController;

public class DeploymentControllerTests
{
    private readonly Mock<IDeploymentService> _configuration = new();

    [Fact]
    public void AddDeploymentStat_Void_ReturnsOk()
    {
        // Arrange
        _configuration.Setup(service => service.AddDeploymentStat());
        // Act
        var controller =
            new account_service.Controllers.DeploymentController.DeploymentController(_configuration.Object);
        
        var value = controller.AddDeploymentStat();
        
        // Assert
        value.Should().BeOfType<OkObjectResult>();
    }
    
    
    [Fact]
    public void AddDeploymentStat_GenericProblemOccured_ReturnsProblem()
    {
        // Arrange
        _configuration.Setup(service => service.AddDeploymentStat())
            .Throws(new Exception("some generic problem occured!"));
        // Act
        var controller =
            new account_service.Controllers.DeploymentController.DeploymentController(_configuration.Object);
        
        var value = controller.AddDeploymentStat();
        
        // Assert
        value.Should().BeOfType<ObjectResult>();
    }
    
    
    
    [Fact]
    public void DeploymentStatList_Void_ReturnsOk()
    {
        // Arrange
        _configuration.Setup(service => service.DeploymentStatList());
        // Act
        var controller =
            new account_service.Controllers.DeploymentController.DeploymentController(_configuration.Object);
        var value = controller.DeploymentStatList();
        // Assert
        value.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public void DeploymentStatList_GenericProblemOccured_ReturnsProblem()
    {
        // Arrange
        _configuration.Setup(service => service.DeploymentStatList())
            .Throws(new Exception("some generic problem occured!"));
        // Act
        var controller =
            new account_service.Controllers.DeploymentController.DeploymentController(_configuration.Object);
        
        var value = controller.DeploymentStatList();
        
        // Assert
        value.Should().BeOfType<ObjectResult>();
    }
    
    
}