using account_service.CustomException;
using account_service.DTO.Painting;
using account_service.Models;
using account_service.Repository.PaintingRepo;
using account_service.Repository.RegistrationRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace account_service.Service.PaintingService;

public class PaintingService : IPaintingService
{
    private readonly IPaintingRepo _paintingRepo;
    private readonly IConfiguration _configuration;   
    private readonly IRegistrationRepo _registrationRepo;

    public PaintingService(IPaintingRepo paintingRepo, IConfiguration configuration)
    {
        _paintingRepo = paintingRepo;
        _configuration = configuration;
    }

    public Painting CreatePainting(Painting painting, Guid painterId)
    {
        painting.PaintingId = Guid.NewGuid();
        painting.PainterId = painterId;
        painting.RegisteredUserId = null; // no buyer yet

        var createdPainting = _paintingRepo.CreatePainting(painting, painterId);

        return createdPainting;
    }

    public IEnumerable<Painting> GetPaintingsForPainter(Guid painterId)
    {
        var paintings = _paintingRepo.GetPaintingsForPainter(painterId);

        return paintings;
    }

    public async Task UploadImage(IFormFile image, string auth0UserId, Guid paintingId)
    {
        var currentPainting = _paintingRepo.GetPaintingInformation(paintingId);

        if (currentPainting == null)
            throw new PaintingDoesNotExist();
        
        string systemFileName = currentPainting.PaintingName + image.FileName;
        currentPainting.PaintingUrl = systemFileName;

        string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
        // Retrieve storage account from connection string.    
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
        // Create the blob client.    
        CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
        // Retrieve a reference to a container.    
        CloudBlobContainer container =
            blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerPaintingName"));
        // This also does not make a service call; it only creates a local object.    
        CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);

        await using (var data = image.OpenReadStream())
        {
            await blockBlob.UploadFromStreamAsync(data);
        }

        await _paintingRepo.AddImageReference(currentPainting, blockBlob.Uri.ToString());
    }

  public  IEnumerable<Painting> GetNRandomPaintingsForSale(int number)
  {
      var paintings = _paintingRepo.GetNRandomPaintingsForSale(number);
      return paintings;
  }

  public Painting GetPaintingByPaintingId(Guid paintingId)
  {
      return _paintingRepo.GetPaintingByPaintingId(paintingId);
  }
}