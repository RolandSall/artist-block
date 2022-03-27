using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("Speciality")]
public class Speciality
{
    [Key]
    [Column("PK_speciality_id")]
    public Guid SpecialityId{ get; set; }
        
    [Required]
    [Column("speciality_type")]
    public string? SpecialityType { get; set; }
    
    public virtual ICollection<PainterSpeciality>? PainterSpecialities { get; set; }
    
}