namespace account_service.Service.BuyController;

public interface IBuyService
{
    void BuyPainting(Guid paintingId, string auth0UserId);
}