﻿using account_service.DTO.PainterSpecialityDto;
using account_service.Models;

namespace account_service.Profile.PainterSpecialityProfile;

public class PainterSpecialityProfile: AutoMapper.Profile
{
    public PainterSpecialityProfile()
    {
        CreateMap<AddPainterSpecialityDto, PainterSpeciality>();
        CreateMap<PainterSpeciality, ReadPainterSpecialityDto>()
            .ForMember(dto => dto.ReadSpecialityDto, opts => opts.MapFrom(p => p.Speciality))

            ;
       
    }

}