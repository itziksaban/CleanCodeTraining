namespace UnitTests.Orders;

internal class Order
{
    public List<string> Items { get; set; }
    public bool CanBeSent { get; set; } = true;
    public string Id { get; set; }
}