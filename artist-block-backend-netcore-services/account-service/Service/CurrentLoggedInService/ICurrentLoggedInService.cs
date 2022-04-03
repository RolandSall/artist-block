using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Service.CurrentLoggedInService;

public interface ICurrentLoggedInService
{
    CurrentUser GetCurrentLoggedInUser(string auth0UserId);
    Painter GetCurrentLoggedInPainterInfo(string auth0UserId);
}