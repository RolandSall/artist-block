using account_service.DTO.Registration;
using account_service.Models;

namespace account_service.Profile.PainterProfile;

public class PainterProfile : AutoMapper.Profile
{
    public PainterProfile()
    {
        CreateMap<CreatePainterDto, Painter>()
            .ForMember(p => p.RegisteredUser, opts => opts.MapFrom(pdto => pdto.CreateClientDto))
            .ForPath(p => p.PainterSpecialities, opts => opts.MapFrom(pdto => pdto.AddPainterSpecialityDtos));
            
            CreateMap<Painter, ReadPainterDto>()
            .ForMember(rpd => rpd.readClientDto, opts => opts.MapFrom(p => p.RegisteredUser));
    }
}