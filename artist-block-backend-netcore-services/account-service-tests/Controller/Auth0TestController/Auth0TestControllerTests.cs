using System;
using account_service.Controllers.Auth0TestController;
using FluentAssertions;
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
}