using account_service.DTO.Painting;
using account_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Service.PaintingService;

public interface IPaintingService
{
    Painting CreatePainting(Painting painting , Guid painterId );
    IEnumerable<Painting> GetPaintingsForPainter(Guid painterId);
    Task UploadImage(IFormFile image, string auth0UserId, Guid paintingId);
    IEnumerable<Painting> GetNRandomPaintingsForSale(int number);
}