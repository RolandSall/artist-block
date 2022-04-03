using account_service.Models;

namespace account_service.Service.RegistrationService;

public interface IRegistrationService
{
    RegisteredUser RegisterClient(RegisteredUser registeredUser, string auth0UserId);
    Painter RegisterPainter(Painter painter, string auth0UserId);
    Painter GetPainterById(Guid painterId);
    Task UploadImage(IFormFile image, string auth0UserId);
}