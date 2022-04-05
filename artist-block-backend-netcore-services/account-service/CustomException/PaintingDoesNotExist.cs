namespace account_service.CustomException;

public class PaintingDoesNotExist : Exception
{
    public string? message { get; }

    public PaintingDoesNotExist()
    {
    }

    public PaintingDoesNotExist(string? message) : base(message)
    {
        this.message = message;
    }
}