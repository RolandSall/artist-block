using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

public class PaintingReview
{
    [Key]
    [Column("PK_painting_review_id")]
    public Guid PaintingReviewId { get; set; }
        
    [Required]
    [Column("timestamp")]
    public DateTime? Timestamp { get; set; }
        
    [Required]
    [Column("like_status")]
    public bool LikeStatus { get; set; }
    
    [Required]
    [Column("comment")]
    public string? Comment { get; set; }
    
    [Column("FK_painting_review_registered_user_id")]
    [Required]
    public Guid? RegisteredUserId { get; set; }
    
    [Column("FK_painting_review_painting_id")]
    [Required]
    public Guid PaintingId { get; set; }
    
    public virtual RegisteredUser? RegisteredUser { get; set; }
    public virtual Painting? Painting { get; set; }
}