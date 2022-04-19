namespace account_service.CustomException;

public class PaintingReviewAlreadyPresentException : Exception
{
    public PaintingReviewAlreadyPresentException(string message) : base(message) {}
    public PaintingReviewAlreadyPresentException() {} 
}