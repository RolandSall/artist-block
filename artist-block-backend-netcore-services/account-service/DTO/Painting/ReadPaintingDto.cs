using System.ComponentModel.DataAnnotations;
using account_service.DTO.Registration;

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
    public string? PaintingStatus { get; set; }
    
    public DateTime? BoughtTimeStamp { get; set; }
    
    public string? PaintingUrl { get; set; }
    
    public DateTime? BuyTimeStamp { get; set; }

    [Required]
    public Guid? RegisteredUserId { get; set; }

    [Required]
    public Guid PainterId { get; set; }
    

    public ReadPainterDto? painterInfo { get; set; }

    //
    // [Column("FK_painting_registered_user_id")]
    // public Guid RegisteredUserId { get; set; }
    //
    //
    // [Column("FK_painting_painter_id")]
    // [Required]
    // public Guid PainterId { get; set; }

}