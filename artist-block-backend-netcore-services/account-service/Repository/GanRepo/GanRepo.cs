using account_service.Models;

namespace account_service.Repository.GanRepo;

public class GanRepo: IGanRepo
{
    private readonly ArtistBlockDbContext _context;

    public GanRepo(ArtistBlockDbContext context)
    {
        _context = context;
    }

    public GanGeneratedImage GetPaintingInformation(Guid ganPaintingId)
    {
        return _context.GanGeneratedImages.FirstOrDefault(gan => gan.GanImageId.Equals(ganPaintingId));
    }

    public Task AddGanImageReference(GanGeneratedImage currentGanImage, string url)
    {
        currentGanImage.ImageUrl = url;
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public void ClaimGanImage(GanGeneratedImage generatedImage)
    {
        _context.GanGeneratedImages.Add(generatedImage);
        _context.SaveChanges();
    }
}