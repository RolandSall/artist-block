namespace account_service.CustomException;

public class ContentNotFoundById : Exception 
{
    public ContentNotFoundById() { }
    public ContentNotFoundById(string message) : base(message) {}
}