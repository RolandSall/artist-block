using System.Reflection.Metadata.Ecma335;
using account_service.DTO.Painting;
using account_service.Models;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository.CreatePaintingRepo;

public class CreatePaintingRepo : ICreatePaintingRepo
{
    private readonly ArtistBlockDbContext _context;
    private ICreatePaintingRepo _createPaintingRepoImplementation;

    public CreatePaintingRepo(ArtistBlockDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public Painting CreatePainting(Painting painting , Guid painterId)
    {
        // make sure the painting's painter exists
        var painter = _context.Painters.FirstOrDefault(painter => painter.PainterId == painterId);
        
        //TODO: handle the case where there is an identical painting in the db

        if (painter == null)
            throw new PainterDoesNotExistException("does not exist");
        
        // painting can be added safely
        var createdPainting = _context.Paintings.Add(painting).Entity;

        _context.SaveChanges();
        return createdPainting;
    }
}