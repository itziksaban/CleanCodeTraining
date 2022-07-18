namespace OnlineStore.Reservations;

public class DeliverableOrder : Order
{
    public string Destination { get; set; }
    public DeliveryType DeliveryType { get; set; }
}