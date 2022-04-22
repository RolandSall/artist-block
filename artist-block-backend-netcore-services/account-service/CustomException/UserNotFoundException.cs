namespace account_service.CustomException;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() {}

    public UserNotFoundException(string? message) : base(message) {}
}
