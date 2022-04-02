using account_service.Models;

namespace account_service.Service.SpecialityService;

public interface ISpecialityService
{
    public Speciality AddSpeciality(Speciality speciality);
    IEnumerable<Speciality> GetAllSpecialities();
}