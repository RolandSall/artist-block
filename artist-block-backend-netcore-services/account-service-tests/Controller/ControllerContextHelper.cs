using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace account_service_tests.Controller;

public static class ControllerContextHelper
{
    public static ControllerContext getStubControllerContext()
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
}