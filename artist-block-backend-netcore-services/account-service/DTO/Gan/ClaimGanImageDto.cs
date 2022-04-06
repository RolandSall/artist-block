using System.ComponentModel.DataAnnotations;

namespace account_service.DTO.Gan;

public class ClaimGanImageDto
{
    [Required]
    public string?  Description { get; set; }
}