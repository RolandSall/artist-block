using account_service.Models;
using account_service.Repository.RegistrationRepo;

namespace account_service.Repository.SpecialityRepo;

public class SpecialityRepo: ISpecialityRepo
{

    private readonly ArtistBlockDbContext _context;

    public SpecialityRepo(ArtistBlockDbContext context)
    {
        _context = context;
    }

    public Speciality AddSpeciality(Speciality speciality)
    {
        var doesExistBefore = _context.Specialities.FirstOrDefault(spec =>
            spec.SpecialityType.ToLower().Equals(speciality.SpecialityType.ToLower()));

        if (doesExistBefore != null)
        {
            throw new SpecialityAlreadyExistException("Speciality Already Exist");
        }


        var savedSpeciality = _context.Specialities.Add(speciality).Entity;
        _context.SaveChanges();
        return savedSpeciality;
    }

    public IEnumerable<Speciality> GetAllSpecialities()
    {
        return _context.Specialities.ToList();
    }
}