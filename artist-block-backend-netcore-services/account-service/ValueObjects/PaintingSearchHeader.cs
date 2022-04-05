namespace account_service.ValueObjects;

public class PaintingSearchHeader
{
    public Guid? PaintingId { get; set; }
    public string? PaintingName { get; set; }
    public string? PaintingYear { get; set; }
}