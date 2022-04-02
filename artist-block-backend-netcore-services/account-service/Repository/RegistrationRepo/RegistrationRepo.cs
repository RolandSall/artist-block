using account_service.Models;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;

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
    
    public RegisteredUser GetUserInfromation(string auth0UserId)
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
}