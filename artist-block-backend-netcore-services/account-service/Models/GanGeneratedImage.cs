using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("gan_image")]
public class GanGeneratedImage
{
    [Key]
    [Column("PK_gan_image_id")]
    public Guid GanImageId{ get; set; }
        
    [Required]
    [Column("gan_image_description")]
    public string?  Description { get; set; }
    
    [Required]
    [Column("gan_image_url")]
    public string?  ImageUrl { get; set; }  
    
    
    [Column("FK_painting_registered_user_id")]
    public Guid? RegisteredUserId { get; set; }
    

}