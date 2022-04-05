using account_service.CustomException;
using account_service.Models;
using account_service.Repository.GanRepo;
using account_service.Service.CurrentLoggedInService;
using account_service.Service.RegistrationService;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace account_service.Service.GanService;

public class GanService: IGanService
{
    private readonly IGanRepo _ganRepo;
    private readonly IConfiguration _configuration;
    private readonly ICurrentLoggedInService _currentLoggedInService;

    public GanService(IGanRepo ganRepo, IConfiguration configuration, ICurrentLoggedInService currentLoggedInService)
    {
        _ganRepo = ganRepo;
        _configuration = configuration;
        _currentLoggedInService = currentLoggedInService;
    }

    public async Task UploadImage(IFormFile image, string auth0UserId, Guid ganPaintingId)
    {
        var currentLoggedInUser = _currentLoggedInService.GetCurrentLoggedInUser(auth0UserId);
        var currentGanImage = _ganRepo.GetPaintingInformation(ganPaintingId);

        if (currentGanImage == null)
        {
            throw new GanGeneratedImageNotFoundException("Generated Image Not Found");
        }
        
        string systemFileName = currentLoggedInUser.RegisteredUser.RegisteredUserId.ToString() + '-' + image.FileName + '-' + currentGanImage.GanImageId;
        currentGanImage.ImageUrl = systemFileName;
        
            string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");  
            // Retrieve storage account from connection string.    
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);  
            // Create the blob client.    
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();  
            // Retrieve a reference to a container.    
            CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerGanPaintingName"));  
            // This also does not make a service call; it only creates a local object.    
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
            
            await using(var data = image.OpenReadStream()) {  
                await blockBlob.UploadFromStreamAsync(data);  
            }
        
            await _ganRepo.AddGanImageReference(currentGanImage, blockBlob.Uri.ToString());
        
        
    }

    public Guid ClaimGanImage(string? description, string auth0UserId)
    {
        Guid ganPaintingId = Guid.NewGuid();
        var RegisteredIdForCurrentLoggedInUser = _currentLoggedInService.GetCurrentLoggedInUser(auth0UserId).RegisteredUser.RegisteredUserId;
        GanGeneratedImage ganGeneratedImage = new GanGeneratedImage()
        {
            Description = description,
            GanImageId = ganPaintingId,
           RegisteredUserId = RegisteredIdForCurrentLoggedInUser,
           //TODO: Remove Required Field for Image URL at This stage
           ImageUrl = "",
        };
        var claimGanImageId = _ganRepo.ClaimGanImage(ganGeneratedImage);
        return claimGanImageId;
    }

    public IEnumerable<GanGeneratedImage> GetAllClaimedGanImagesForClient(string auth0UserId)
    {
        var userId = _currentLoggedInService.GetCurrentLoggedInUser(auth0UserId).RegisteredUser.RegisteredUserId;
        return _ganRepo.GetAllClaimedGanImagesForClient(userId);
    }
}