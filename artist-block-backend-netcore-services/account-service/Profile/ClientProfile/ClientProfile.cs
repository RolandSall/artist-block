using account_service.DTO.Registration;
using account_service.Models;

namespace account_service.Profile.ClientProfile;

public class ClientProfile: AutoMapper.Profile
{
    public ClientProfile()
    {
        CreateMap<CreateClientDto, RegisteredUser>();
        CreateMap<RegisteredUser, ReadClientDto>();
    }
}