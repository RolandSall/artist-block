using account_service.CustomException;
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

    public Guid ClaimGanImage(GanGeneratedImage generatedImage)
    {
        var painting = _context.GanGeneratedImages.Add(generatedImage).Entity;
        _context.SaveChanges();
        return painting.GanImageId;
    }

    public IEnumerable<GanGeneratedImage> GetAllClaimedGanImagesForClient(Guid userId)
    {
        var ganList = _context.GanGeneratedImages.Where(ganImg => ganImg.RegisteredUserId.Equals(userId));
        return ganList;
    }

    public GanGeneratedImage GetGanImageById(Guid id)
    {
        
        var ganImage = _context.GanGeneratedImages.FirstOrDefault(image => image.GanImageId.Equals(id));

        if (ganImage == null)
            throw new ContentNotFoundById($"could not find gan image with id: {id} !");

        return ganImage;
    }
}