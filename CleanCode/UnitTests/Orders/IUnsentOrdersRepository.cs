namespace UnitTests.Orders;

internal interface IUnsentOrdersRepository
{
    Task<List<Order>> GetAll();
    void Remove(string orderId);
}