namespace account_service.CustomException;

public class RegistrationFailedException : Exception
{
    public string? message { get; }

    public RegistrationFailedException()
    {
    }

    public RegistrationFailedException(string? message) : base(message)
    {
        this.message = message;
    }
}
