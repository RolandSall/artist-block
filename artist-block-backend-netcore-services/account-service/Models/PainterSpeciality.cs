using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("painter_speciality")]
public class PainterSpeciality
{
  
        [Key]
        [Column("PK_painter_speciality_id")]
        public Guid PainterSpecialityId{ get; set; }
        
        
        [Column("FK_speciality_id")]
        public Guid SpecialityId { get; set; }

        [Column("FK_painter_id")]
        public Guid PainterId { get; set; }
       
        [Column("priority")]
        public int Priority { get; set; }
        
        
        
        
        
        
    
}