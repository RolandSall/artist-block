using account_service.Models;

namespace account_service.Service.GanService;

public interface IGanService
{
    Task UploadImage(IFormFile image, string auth0UserId, Guid ganPaintingId);
    Guid ClaimGanImage(string? description, string auth0UserId);
    IEnumerable<GanGeneratedImage> GetAllClaimedGanImagesForClient(string auth0UserId);
}