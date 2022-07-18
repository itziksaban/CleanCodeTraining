using System.Collections;
using System.Collections.Generic;

namespace OnlineStore.Reservations;

public class Order
{
    public OrderType OrderType { get; set; }
    public IEnumerable<OrderLine> OrderLines { get; set; }
}