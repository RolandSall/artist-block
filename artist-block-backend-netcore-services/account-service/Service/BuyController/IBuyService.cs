using account_service.Controllers.BuyController;

namespace account_service.Service.BuyController;

public interface IBuyService
{
    void BuyPainting(Guid paintingId, string auth0UserId);
    void SellPainting(Guid paintingId, string auth0UserId, PaintingStatusRequest request);
}