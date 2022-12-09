namespace UnitTests.Orders;

public interface IWarehouseRepository
{
    bool ItemAvailable(string itemId);
}