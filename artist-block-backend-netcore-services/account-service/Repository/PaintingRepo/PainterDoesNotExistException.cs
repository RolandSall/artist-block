namespace account_service.Repository.PaintingRepo;

public class PainterDoesNotExistException : Exception
{
    public PainterDoesNotExistException(string? message ) : base(message)
    {}
}