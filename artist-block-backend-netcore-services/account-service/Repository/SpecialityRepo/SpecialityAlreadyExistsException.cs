namespace account_service.Repository.RegistrationRepo;

public class SpecialityAlreadyExistsException: Exception
{
    public string? message { get;  }

    public SpecialityAlreadyExistsException()
    {
    }
    
    public SpecialityAlreadyExistsException(string? message) : base(message)
    {
        this.message = message;
    }
    
    
}