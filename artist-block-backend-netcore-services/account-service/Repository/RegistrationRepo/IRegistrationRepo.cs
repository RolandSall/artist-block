using account_service.Models;

namespace account_service.Repository.RegistrationRepo;

public interface IRegistrationRepo
{
    RegisteredUser RegisterClient(RegisteredUser registeredUser, string auth0UserId);
    Painter RegisterPainter(Painter registeredPainter);
    Painter GetPainterById(Guid painterId);
}