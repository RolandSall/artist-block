using System;
using System.Security.Claims;
using account_service.Controllers.Auth0TestController;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class Auth0TestControllerTests
{
    
    [Fact]
    public void Public_Void_ReturnOkStatus()
    {
        // Arrange
        var controller = new Auth0TestController();
        
        // Act
        var returnedValue = controller.Public();
        
        // Assert
        returnedValue.Should().BeOfType<OkObjectResult>();

        var returned = (returnedValue as OkObjectResult).Value;

        returned.Should()
            .BeEquivalentTo("Hello from a public endpoint! You don't need to be authenticated to see this!!!");
    }

    [Fact]
    public void Private_Void_ReturnsOkWithAuth0Id()
    {
        // Arrange
        
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
        var controller = new Auth0TestController {ControllerContext = controllerContext};
        
        // create expected value
        var expectedId = "auth0|123123123123";
        var expectedMessage = "Hello from a private endpoint! You need to be authenticated to see this!";
        
        var expectedValue = new 
        {
            Message = expectedMessage,
            Id = expectedId,
        };

        // Act
        var returnedValue = (controller.Private() as OkObjectResult).Value;

        // Assert
        
        returnedValue.Should().BeEquivalentTo(expectedValue);
    }
}