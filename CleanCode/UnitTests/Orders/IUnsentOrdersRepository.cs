namespace UnitTests.Orders;

public interface IUnsentOrdersRepository
{
    Task<List<Order>> GetAll();
    void Remove(string orderId);
}