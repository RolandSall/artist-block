using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Repository.RegistrationRepo;

public interface IRegistrationRepo
{
    RegisteredUser RegisterClient(RegisteredUser registeredUser, string auth0UserId);
    Painter RegisterPainter(Painter registeredPainter);
    Painter GetPainterById(Guid painterId);
    RegisteredUser GetUserInfromation(string auth0UserId);
    Task AddImageReference(RegisteredUser currentUser, string toString);
    Guid DeleteUserById(Guid registeredClientRegisteredUserId);
    CurrentUser GetCurrentLoggedInUser(string auth0UserId);
    Guid GetCurrentLoggedInPainterInfo(string auth0UserId);
}