using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("registered_user")]
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
        
    [Required]
    [Phone]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }
        
    [Required]
    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }

        
}