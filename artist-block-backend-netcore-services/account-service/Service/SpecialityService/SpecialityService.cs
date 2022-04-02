using account_service.Models;
using account_service.Repository.SpecialityRepo;

namespace account_service.Service.SpecialityService;

public class SpecialityService: ISpecialityService
{

    private readonly ISpecialityRepo _specialityRepo;

    public SpecialityService(ISpecialityRepo specialityRepo)
    {
        _specialityRepo = specialityRepo;
    }

    public Speciality AddSpeciality(Speciality speciality)
    {
        Guid specialityId = new Guid();
        speciality.SpecialityId = specialityId;
        var AddedSpeciality = _specialityRepo.AddSpeciality(speciality);
        return AddedSpeciality;
    }

    public IEnumerable<Speciality> GetAllSpecialities()
    {
        return _specialityRepo.GetAllSpecialities();
    }
}