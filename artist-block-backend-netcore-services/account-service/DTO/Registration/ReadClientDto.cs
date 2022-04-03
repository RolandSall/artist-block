using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.DTO.Registration;

public class ReadClientDto
{
    [Key]
    public Guid RegisteredUserId { get; set; }
        
    [Required]
    public string? FirstName { get; set; }
        
    [Required]
    public string? LastName { get; set; }
        
    [Required]
    public string? Title { get; set; }
        
    [Required]
    public string?  Nationality { get; set; }
        
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
        
    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }
    
    public string? Image { get; set; }
        
    [Required]
    public DateTime? BirthDate { get; set; }
}