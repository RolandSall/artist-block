using account_service.DTO.Painting;

namespace account_service.Profile.PaintingProfile;

public class PaintingProfile : AutoMapper.Profile
{
    public PaintingProfile()
    {
        // Source -> Dest
        CreateMap<CreatePaintingDto, Models.Painting>();
            // .ForMember(p => p.RegisteredUser, opts => opts.MapFrom(pdto => pdto.CreateClientDto));

        CreateMap<Models.Painting, ReadPaintingDto>();
            // .ForMember(rpd => rpd.readClientDto, opts => opts.MapFrom(p => p.RegisteredUser));
    }
}