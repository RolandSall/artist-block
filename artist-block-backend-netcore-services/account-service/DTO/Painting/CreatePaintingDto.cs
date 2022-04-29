using System.ComponentModel.DataAnnotations;

namespace account_service.DTO.Painting;

public class CreatePaintingDto
{
    [Required]
    public string? PaintingName { get; set; }
    
    [Required]
    public string? PaintingDescription { get; set; }
    
    [Required]
    public string? PaintedYear { get; set; }
    
    [Required]
    public int? PaintingPrice { get; set; }
    
    [Required]
    // on sale, sold ...
    public string? PaintingStatus { get; set; }
    
    
    public DateTime? BuyTimeStamp { get; set; }
    
}