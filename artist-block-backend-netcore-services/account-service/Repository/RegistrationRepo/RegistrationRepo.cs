using account_service.CustomException;
using account_service.Models;
using account_service.ValueObjects;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository.RegistrationRepo;

public class RegistrationRepo: IRegistrationRepo
{
    
    private readonly ArtistBlockDbContext _context;
    private readonly IMapper _mapper;

    public RegistrationRepo(ArtistBlockDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        
        // This is needed just to get a full response to join the tables
        // if you do not want to show the full response ( the specialities how they map to him)
        // no need to do the extra call below.
       

        return GetPainterById(registeredPainter.PainterId);;
    }

    public Painter GetPainterById(Guid painterId)
    {
        var painter = _context.Painters.Where(painter => painter.PainterId.Equals(painterId))
            .Include(painter => painter.RegisteredUser)
            .Include(painter => painter.PainterSpecialities)
            .ThenInclude(ps => ps.Speciality)
            
            
            
            .FirstOrDefault();
        return painter;
    }
    
    public RegisteredUser GetUserInformation(string auth0UserId)
    {
        var ru = _context.AuthUsers
            .Where(auth => auth.Auth0Id.Equals(auth0UserId))
            .Include(auth => auth.RegisteredUser)
            .FirstOrDefault().RegisteredUser;

        return ru;
    }

    public Task AddImageReference(RegisteredUser currentUser, string ImageUrl)
    {
        currentUser.Image = ImageUrl;
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Guid DeleteUserById(Guid registeredClientRegisteredUserId)
    {
        var user = _context.RegisteredUsers.First(ru => ru.RegisteredUserId.Equals(registeredClientRegisteredUserId));
        _context.RegisteredUsers.Remove(user);
        _context.SaveChanges();
        return registeredClientRegisteredUserId;
    }

    public CurrentUser GetCurrentLoggedInUser(string auth0UserId)
    {
        // Get the User ID from the auth0 Identity
        var auth0 = _context.AuthUsers
            .Where(auth => auth.Auth0Id.Equals(auth0UserId))
            .Include(auth => auth.RegisteredUser)
            .FirstOrDefault();

      
        var currentUser = _mapper.Map<AuthUser, CurrentUser>(auth0);
            
        if (auth0 == null)
        {
            throw new UserNotFoundException("User Not Registered");
        }
            
        // if the ru exist in the painter table as FK then this user is a painter
        var painterEntity = _context.Painters
            .FirstOrDefault(painter => painter.RegisteredUserId.Equals(auth0.RegisteredUserId));
            
           
        if (painterEntity == null) {
            currentUser.Role = "Client";
            return currentUser;
        } else {
            currentUser.Role = "Painter";
            return currentUser;
        }
    }

    public Guid GetCurrentLoggedInPainterInfo(string auth0UserId)
    {
        var auth0 = _context.AuthUsers
            .FirstOrDefault(auth => auth.Auth0Id.Equals(auth0UserId));
            
        var painterEntity = _context.Painters
            .FirstOrDefault(painter => painter.RegisteredUserId.Equals(auth0.RegisteredUserId));
            
        if (auth0 == null)
        {
            throw new UserNotFoundException("User Not Registered");
        }
            
        if (painterEntity == null)
        {
            throw new UserNotFoundException("Not Registered As A Painter");
        }

        return painterEntity.PainterId;
    }
}