using account_service.DTO.Speciality;

namespace account_service.DTO.PainterSpecialityDto;

public class ReadPainterSpecialityDto
{

    public Guid PainterSpecialityId{ get; set; }
        
        
    public ReadSpecialityDto ReadSpecialityDto { get; set; }

    public int Priority { get; set; }
}