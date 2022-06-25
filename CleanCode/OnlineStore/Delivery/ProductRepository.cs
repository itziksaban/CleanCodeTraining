namespace OnlineStore.Reservations;

public class ProductRepository
{
    public Product GetProduct(string productId)
    {
        //Dummy implementation - just for demonstration
        return DummyProduct;
    }

    public Product DummyProduct { get; set; }
}