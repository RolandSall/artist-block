using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using account_service.DTO.PainterSpecialityDto;

namespace account_service.DTO.Registration;

public class ReadPainterDto
{
    public Guid PainterId { get; set; }
    public string?  Location { get; set; }
    public string?  YearsOfExperience { get; set; }
    public string?  Bio { get; set; }
    
    [JsonPropertyName("client")]
    public virtual ReadClientDto? readClientDto { get; set; }
    
     
    public virtual ICollection<ReadPainterSpecialityDto>? PainterSpecialityDtos { get; set; }
}