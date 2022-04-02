using account_service.CustomException;
using account_service.Models;
using account_service.Repository.RegistrationRepo;
using AutoMapper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace account_service.Service.RegistrationService;

public class RegistrationService: IRegistrationService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IRegistrationRepo _registrationRepo;

    public RegistrationService(IConfiguration configuration, IMapper mapper, IRegistrationRepo registrationRepo)
    {
        _configuration = configuration;
        _mapper = mapper;
        _registrationRepo = registrationRepo;
    }


    public RegisteredUser RegisterClient(RegisteredUser registeredUser, string auth0UserId)
    {
        var registeredUserId = Guid.NewGuid();
        registeredUser.RegisteredUserId = registeredUserId;
        var savedRegisteredUser = _registrationRepo.RegisterClient(registeredUser,auth0UserId);
        return savedRegisteredUser;
    }

    public Painter RegisterPainter(Painter painter, string auth0UserId)
    {
        var painterId = Guid.NewGuid();
        // TODO : automatic add 
        //registeredPainter.RegisteredUserId = Guid.NewGuid();
        
        
        // Register as Client
        painter.PainterId = painterId;
        var registeredClient = RegisterClient(painter.RegisteredUser, auth0UserId);
        painter.RegisteredUserId = registeredClient.RegisteredUserId;
        
        // assign for each speciality a unique Id to to the db and give FK to the newly generated lawyerId
        foreach (var painterSpeciality in painter.PainterSpecialities)
        {
            Guid painterSpecialityId = Guid.NewGuid();;
            painterSpeciality.PainterSpecialityId = painterSpecialityId;
            painterSpeciality.PainterId = painterId;
        }
        
        try
        {
            var registeredPainter = _registrationRepo.RegisterPainter( painter );
            return registeredPainter;
        }
        catch
        {
            Guid uuid = _registrationRepo.DeleteUserById(registeredClient.RegisteredUserId);
            throw new RegistrationFailedException("Failed To Complete Lawyer Registration. Please Try Again");
        }
    
      
    }

    public Painter GetPainterById(Guid painterId)
    {
        return _registrationRepo.GetPainterById(painterId);
    }

    public async Task UploadImage(IFormFile image, string auth0UserId)
    {
        var currentUser = _registrationRepo.GetUserInfromation(auth0UserId);
        string systemFileName = currentUser.RegisteredUserId + image.FileName;
        currentUser.Image = systemFileName;
        
        string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");  
        // Retrieve storage account from connection string.    
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);  
        // Create the blob client.    
        CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();  
        // Retrieve a reference to a container.    
        CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerImageName"));  
        // This also does not make a service call; it only creates a local object.    
        CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
            
        await using(var data = image.OpenReadStream()) {  
            await blockBlob.UploadFromStreamAsync(data);  
        }
        
        await _registrationRepo.AddImageReference(currentUser, blockBlob.Uri.ToString());
        
    }
}