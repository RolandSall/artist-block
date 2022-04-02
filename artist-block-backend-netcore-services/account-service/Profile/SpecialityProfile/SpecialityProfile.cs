using account_service.DTO.Speciality;
using account_service.Models;

namespace account_service.Profile.SpecialityProfile;

public class SpecialityProfile: AutoMapper.Profile
{
    public SpecialityProfile()
    {
        CreateMap<CreateSpecialityDto, Speciality>();
        CreateMap<Speciality, ReadSpecialityDto>();
    }
    
}