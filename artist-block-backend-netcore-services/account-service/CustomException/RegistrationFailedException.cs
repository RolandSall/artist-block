namespace account_service.CustomException;

public class RegistrationFailedException : Exception
{
    public RegistrationFailedException() {}

    public RegistrationFailedException(string? message) : base(message) {}
}
