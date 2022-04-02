using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using account_service.DTO.PainterSpecialityDto;
using account_service.Models;

namespace account_service.DTO.Registration;

public class CreatePainterDto
{
    [Required]
    public string?  Location { get; set; }
    
    [Required]
    public string?  YearsOfExperience { get; set; }
    
    [Required]
    public string?  Bio { get; set; }
    
    [JsonPropertyName("client")]
    public virtual CreateClientDto? CreateClientDto { get; set; }
    
    public virtual ICollection<AddPainterSpecialityDto>? AddPainterSpecialityDtos { get; set; }
    
    
}