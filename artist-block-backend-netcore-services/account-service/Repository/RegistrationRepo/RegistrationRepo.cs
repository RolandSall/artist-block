using account_service.Models;

namespace account_service.Repository.RegistrationRepo;

public class RegistrationRepo: IRegistrationRepo
{
    
    private readonly ArtistBlockDbContext _context;

    public RegistrationRepo(ArtistBlockDbContext context)
    {
        _context = context;
    }

    public RegisteredUser RegisterClient(RegisteredUser registeredUser, string auth0UserId)
    {

        var Auth0IdExist = _context.AuthUsers.FirstOrDefault(auth => auth.Auth0Id.Equals(auth0UserId));

        if (Auth0IdExist != null)
        {
            throw new ClientAlreadyExistException("User Already Registered");
        }
        
        var auth0User = new AuthUser();
        var savedRegisteredUser = _context.Add(registeredUser).Entity;
        
        auth0User.RegisteredUserId = savedRegisteredUser.RegisteredUserId;
        auth0User.Auth0Id = auth0UserId;
        
        var savedAuthUser = _context.AuthUsers.Add(auth0User).Entity;
        _context.SaveChanges();
        return savedRegisteredUser;
    }

    public Painter RegisterPainter(Painter painter)
    {
        var registeredPainter = _context.Painters.Add(painter).Entity;

        _context.SaveChanges();
        return registeredPainter;
    }
}