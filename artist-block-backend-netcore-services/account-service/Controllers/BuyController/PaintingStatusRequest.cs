using System.ComponentModel.DataAnnotations;
using OpenTelemetry.Trace;

namespace account_service.Controllers.BuyController;

public class PaintingStatusRequest
{
    [Required]
    public string? Status { get; set; }

   
    public int? Price { get; set; }

    //TODO: added buy out timeline
    
}