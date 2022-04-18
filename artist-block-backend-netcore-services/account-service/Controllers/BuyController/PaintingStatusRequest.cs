using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenTelemetry.Trace;

namespace account_service.Controllers.BuyController;

public class PaintingStatusRequest
{
    [Required]
    public string? Status { get; set; }

   
    public int? Price { get; set; }
    

    public DateTime? BuyTimeStamp { get; set; }
    
}