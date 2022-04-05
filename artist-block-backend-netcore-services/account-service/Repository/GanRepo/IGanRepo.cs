using account_service.Models;

namespace account_service.Repository.GanRepo;

public interface IGanRepo
{
    GanGeneratedImage GetPaintingInformation(Guid ganPaintingId);
    Task AddGanImageReference(GanGeneratedImage currentGanImage, string url);
    void ClaimGanImage(GanGeneratedImage ganGeneratedImage);
}