using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Orders
{
    public class DiscountCalculator
    {
        public double Calc(Order order, User user)
        {
            if (order.Items.Count > 10 || user.IsVIP)
            {
                return 0.05 * order.TotalPrice;
            }

            return 0;
        }
    }
}
