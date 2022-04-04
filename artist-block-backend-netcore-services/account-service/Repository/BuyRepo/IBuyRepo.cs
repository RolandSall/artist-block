using account_service.Controllers.BuyController;

namespace account_service.Repository.BuyRepo;

public interface IBuyRepo
{
    void BuyPainting(Guid paintingId, Guid userId);
    void SellPainting(Guid paintingId, Guid userId, PaintingStatusRequest paintingStatusRequest);
}