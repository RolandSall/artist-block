namespace account_service.Repository.RegistrationRepo;

public class SpecialityAlreadyExistException: Exception
{
    public string? message { get;  }

    public SpecialityAlreadyExistException()
    {
    }
    
    public SpecialityAlreadyExistException(string? message) : base(message)
    {
        this.message = message;
    }
    
    
}