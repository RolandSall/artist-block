using account_service.DTO.Current;
using account_service.Models;
using account_service.ValueObjects;

namespace account_service.Profile;

public class CurrentProfile: AutoMapper.Profile

{
    public CurrentProfile()
    {
        CreateMap<AuthUser, CurrentUser>()
            .ForMember(ur => ur.RegisteredUser, opts => opts.MapFrom(au => au.RegisteredUser));

        CreateMap<CurrentUser, ReadCurrentLoggedInUserDto>()
            .ForMember(rcDto => rcDto.ReadClientDto, opts => opts.MapFrom(ur => ur.RegisteredUser));


    }
}