namespace account_service.ValueObjects;

public class PainterSearchHeader
{
    public Guid? PainterId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PainterUrl { get; set; }
}