namespace UnitTests.Orders;

internal interface IWarehouseRepository
{
    bool ItemAvailable(string itemId);
}

internal class WarehouseRepository : IWarehouseRepository
{
    public bool ItemAvailable(string itemId)
    {
        throw new NotImplementedException();
    }
}