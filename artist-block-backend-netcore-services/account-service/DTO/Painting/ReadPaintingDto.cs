using System.ComponentModel.DataAnnotations;

namespace account_service.DTO.Painting;

public class ReadPaintingDto
{
    [Key]
    public Guid PaintingId{ get; set; }
    
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
    public int? PaintingStatus { get; set; }
    
    
    //
    // [Column("FK_painting_registered_user_id")]
    // public Guid RegisteredUserId { get; set; }
    //
    //
    // [Column("FK_painting_painter_id")]
    // [Required]
    // public Guid PainterId { get; set; }
    
}