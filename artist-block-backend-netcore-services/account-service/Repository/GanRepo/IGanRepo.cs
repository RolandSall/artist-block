using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Repository.GanRepo;

public interface IGanRepo
{
    GanGeneratedImage GetPaintingInformation(Guid ganPaintingId);
    Task AddGanImageReference(GanGeneratedImage currentGanImage, string url);
    void ClaimGanImage(GanGeneratedImage ganGeneratedImage);
    IEnumerable<GanGeneratedImage> GetAllClaimedGanImagesForClient(Guid userId);
}