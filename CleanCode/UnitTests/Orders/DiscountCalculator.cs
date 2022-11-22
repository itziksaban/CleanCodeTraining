using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Orders
{
    public class DiscountCalculator
    {
        public void Calc(Order order, User user)
        {
            if (order.Items.Count > 10 || user.IsVIP)
            {
                Order.Discount = 0.05;
            }
        }
    }
}
