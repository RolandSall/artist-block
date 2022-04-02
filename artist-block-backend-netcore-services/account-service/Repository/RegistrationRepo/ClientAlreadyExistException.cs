namespace account_service.Repository.RegistrationRepo;

public class ClientAlreadyExistException: System.Exception
{
    public string? message { get;  }

    public ClientAlreadyExistException()
    {
    }
    
    public ClientAlreadyExistException(string? message) : base(message)
    {
        this.message = message;
    }
    
    
}