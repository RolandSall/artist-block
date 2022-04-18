using account_service.DTO.PaintingReview;
using account_service.Models;

namespace account_service.Profile.PaintingReviewProfile;

public class PaintingReviewProfile : AutoMapper.Profile
{
    public PaintingReviewProfile()
    {
        // source -> destination
        CreateMap<CreatePaintingReviewDto, PaintingReview>();
        CreateMap<PaintingReview, ReadPaintingReviewDto>();
    }
}