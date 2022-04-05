namespace account_service.ValueObjects;

public class PainterSearchResult
{
    public virtual ICollection<PainterSearchHeader>? PainterList { get; set; }
    public virtual ICollection<PaintingSearchHeader>? PaintingList { get; set; }
}