using Microsoft.Azure.Cosmos;

namespace UnitTests.Orders;

internal class UnsentOrdersRepository : IUnsentOrdersRepository
{
    private Container _container;

    public UnsentOrdersRepository()
    {
        var cosmosClient = new CosmosClient(COSMOS_URI, COSMOS_KEY);
        _container = cosmosClient.GetContainer(DATABASE_ID, ORDERS_CONTAINER);
    }

    private const string COSMOS_KEY = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    private const string COSMOS_URI = "https://localhost:8081";
    private const string DATABASE_ID = "My_DB";
    private const string ORDERS_CONTAINER = "Orders";

    public async Task<List<Order>> GetAll()
    {
        var sqlQueryText = $"SELECT * FROM orders WHERE orders.status = 'Unsent'";
        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
        using FeedIterator<Order> tasksIterator = _container.GetItemQueryIterator<Order>(queryDefinition);
        var orders = new List<Order>();
        while (tasksIterator.HasMoreResults)
        {
            FeedResponse<Order> currentResultSet = await tasksIterator.ReadNextAsync();
            foreach (Order order in currentResultSet)
            {
                orders.Add(order);
            }
        }
        return orders;
    }

    public void Remove(string orderId)
    {
        _container.DeleteItemAsync<Order>(orderId, PartitionKey.None);
    }
}