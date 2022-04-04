namespace account_service.Repository.BuyRepo;

public interface IBuyRepo
{
    void BuyPainting(Guid guid, Guid paintingId);
}