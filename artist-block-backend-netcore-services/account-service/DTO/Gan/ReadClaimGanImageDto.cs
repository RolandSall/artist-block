using System.ComponentModel.DataAnnotations;

namespace account_service.DTO.Gan;

public class ReadClaimGanImageDto
{
    [Required]
    public Guid GanImageId{ get; set; }
}