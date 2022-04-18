using System.ComponentModel.DataAnnotations;

namespace account_service.DTO.PaintingReview;

public class ReadPaintingReviewDto
{
    [Key]
    public Guid PaintingReviewId { get; set; }
        
    [Required]
    public DateTime? Timestamp { get; set; }
        
    [Required]
    public bool LikeStatus { get; set; }
    
    [Required]
    public string? Comment { get; set; }
    
    [Required]
    public Guid? RegisteredUserId { get; set; }
    
    [Required]
    public Guid PaintingId { get; set; }
}