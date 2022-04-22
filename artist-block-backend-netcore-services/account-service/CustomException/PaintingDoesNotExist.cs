namespace account_service.CustomException;

public class PaintingDoesNotExist : Exception
{
    public PaintingDoesNotExist() {}

    public PaintingDoesNotExist(string? message) : base(message) {}
}