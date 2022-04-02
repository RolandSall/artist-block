namespace account_service.Repository.CreatePaintingRepo;

public class PainterDoesNotExistException : Exception
{
    public PainterDoesNotExistException(string? message ) : base(message)
    {}
}