using System.ComponentModel.DataAnnotations;

namespace account_service.Controllers.SearchController;

public class FindPaintingFilter
{
   
        const int MaxPageSize = 50;
        [Required]
        public int PageNumber { get; set; } = 1;
        public string? PaintingDescription { get; set; }
        public string? PaintingYear { get; set; }
        public int? RateStart { get; set; }
        public int? RateEnd { get; set; }
        private int _pageSize = 10;
        [Required]
        public int PageSize
        
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
      
        
    
}