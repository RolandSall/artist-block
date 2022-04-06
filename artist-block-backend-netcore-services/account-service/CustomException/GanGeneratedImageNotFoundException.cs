namespace account_service.CustomException;

public class GanGeneratedImageNotFoundException : Exception
{
    public string? message { get; }

    public GanGeneratedImageNotFoundException()
    {
    }

    public GanGeneratedImageNotFoundException(string? message) : base(message)
    {
        this.message = message;
    }
}
