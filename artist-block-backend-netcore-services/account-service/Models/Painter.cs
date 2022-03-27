using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("painter")]
public class Painter
{
    [Key]
    [Column("PK_painter_id")]
    public Guid PainterId{ get; set; }
        
    [Required]
    [Column("Location")]
    public string?  Location { get; set; }
      
    [Column("FK_painter_registered_user_id")]
    public Guid RegisteredUserId { get; set; }
        
    public RegisteredUser? RegisteredUser { get; set; }
        
    public virtual ICollection<Painting>? Paintings { get; set; }
    
    public virtual ICollection<PainterSpeciality>? PainterSpecialities { get; set; }
}