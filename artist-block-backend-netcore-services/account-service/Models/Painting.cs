using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("painting")]
public class Painting
{
    [Key]
    [Column("PK_painting_id")]
    public Guid PaintingId{ get; set; }
    
    [Required]
    [Column("painting_name")]
    public string? PaintingName { get; set; }
    
    [Required]
    [Column("painting_description")]
    public string? PaintingDescription { get; set; }
    
    [Required]
    [Column("painted_year")]
    public string? PaintedYear { get; set; }
    
    [Required]
    [Column("painting_price")]
    public int? PaintingPrice { get; set; }
    
    [Required]
    [Column("status")]
    // on sale, sold ...
    public string? PaintingStatus { get; set; }
    
    
    [Column("buy_timestamp")]
    public DateTime? BuyTimeStamp { get; set; }
    
    [Column("bought_timestamp")]
    public DateTime? BoughtTimeStamp { get; set; }
    
    [Column("painting_url")]
    public string? PaintingUrl { get; set; }
    
    [Column("FK_painting_registered_user_id")]
    public Guid? RegisteredUserId { get; set; }
    
    [Column("FK_painting_painter_id")]
    [Required]
    public Guid PainterId { get; set; }
    
    
    public virtual Painter? Painter { get; set; }
}