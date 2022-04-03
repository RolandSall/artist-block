using account_service.Models;

namespace account_service.Repository.SpecialityRepo;

public interface ISpecialityRepo
{
    Speciality AddSpeciality(Speciality speciality);
    IEnumerable<Speciality> GetAllSpecialities();
}