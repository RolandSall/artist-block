using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    // [Column("FK_painting_registered_user_id")]
    // public Guid RegisteredUserId { get; set; }
    //
    // [Column("FK_painting_painter_id")]
    // [Required]
    // public Guid PainterId { get; set; }
}