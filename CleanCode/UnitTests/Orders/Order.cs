namespace UnitTests.Orders;

public class Order
{
    public List<string> Items { get; set; }
    public bool CanBeSent { get; set; } = true;
    public string Id { get; set; }
    public double TotalPrice { get; set; }
    public double Discount { get; set; }
}
