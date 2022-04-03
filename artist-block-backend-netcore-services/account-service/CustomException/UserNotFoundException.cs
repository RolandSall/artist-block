namespace account_service.CustomException;

public class UserNotFoundException : Exception
{
    public string? message { get; }

    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
        this.message = message;
    }
}
