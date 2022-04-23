namespace account_service.ValueObjects;

public struct NumPaintingsAndGan
{
    public int numPaintings { get; set; }
    public int numGans { get; set; }

    public NumPaintingsAndGan(int numPaintings, int numGans)
    {
        this.numPaintings = numPaintings;
        this.numGans = numGans;
    }
}