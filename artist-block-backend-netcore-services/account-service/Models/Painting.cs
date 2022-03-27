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
    [Column("painting_information")]
    public string? PaintingInformation { get; set; }
    
    [Required]
    [Column("painted_year")]
    public string? PaintedYear { get; set; }
    
    [Required]
    [Column("painting_price")]
    public string? PaintingPrice { get; set; }
    
    
    

    
}