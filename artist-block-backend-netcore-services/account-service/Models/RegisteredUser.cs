using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("client")]
public class RegisteredUser {
        
    [Key]
    [Column("PK_registered_user_id")]
    public Guid RegisteredUserId { get; set; }
        
    [Required]
    [Column("first_name")]
    public string? FirstName { get; set; }
        
    [Required]
    [Column("last_name")]
    public string? LastName { get; set; }
        
    [Required]
    [Column("title")]
    public string? Title { get; set; }
        
    [Required]
    [Column("nationality")]
    public string?  Nationality { get; set; }
        
    [Required]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }
    
    [Column("image_url")]
    public string? Image { get; set; }
        
    [Required]
    [Phone]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }
        
    [Required]
    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }
    
    
    public virtual Painter Painter { get; set; }

    
    public virtual ICollection<Painting>? PaintingsBought { get; set; }
    
    public virtual ICollection<GanGeneratedImage>? ClaimedGanImages { get; set; }

        
}