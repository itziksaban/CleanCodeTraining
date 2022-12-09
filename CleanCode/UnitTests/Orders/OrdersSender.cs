using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Orders
{
    public class OrdersSender
    {
        private readonly IUnsentOrdersRepository _unsentOrdersRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public OrdersSender(IUnsentOrdersRepository unsentOrdersRepository, IWarehouseRepository warehouseRepository)
        {
            _unsentOrdersRepository = unsentOrdersRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task Send()
        {
            var unsentOrders = await _unsentOrdersRepository.GetAll();
            unsentOrders.ForEach(order =>
            {
                order.Items.ForEach(itemId =>
                {
                    if (!_warehouseRepository.ItemAvailable(itemId))
                    {
                        order.CanBeSent = false;
                    }
                });
                if (order.CanBeSent)
                {
                    _unsentOrdersRepository.Remove(order.Id);
                }
            });
        }
    }
}