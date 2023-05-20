using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Orders
{
    [TestClass]
    public class OrderSenderTests
    {
        private List<Order> _orders;
        private Mock<IUnsentOrdersRepository> _unsentOrdersRepository;
        private Mock<IWarehouseRepository> _warehouseRepository;
        private OrdersSender _ordersSender;

        public OrderSenderTests()
        {
            _orders = new List<Order>
            {
                new()
                {
                    Items = new List<string>()
                    {
                        "item1"
                    },
                    Id = "order1"
                }
            };
            _unsentOrdersRepository = new Mock<IUnsentOrdersRepository>();
            _warehouseRepository = new Mock<IWarehouseRepository>();
            _ordersSender = new OrdersSender(_unsentOrdersRepository.Object, _warehouseRepository.Object);
        }

        [TestMethod]
        public async Task Send_AllItemsAreAvailable_RemoveFromUnsent()
        {
            _unsentOrdersRepository.Setup(repository => repository.GetAll()).ReturnsAsync(_orders);
            _warehouseRepository.Setup(repository => repository.ItemAvailable("item1")).Returns(true);

            await _ordersSender.Send();

            _unsentOrdersRepository.Verify(repository => repository.Remove("order1"));
        }

        [TestMethod]
        public async Task Send_NotAllItemsAreAvailable_DoNotRemoveFromUnsent()
        {
            _unsentOrdersRepository.Setup(repository => repository.GetAll()).ReturnsAsync(_orders);
            _warehouseRepository.Setup(repository => repository.ItemAvailable("item1")).Returns(false);

            await _ordersSender.Send();

            _unsentOrdersRepository.Verify(repository => repository.Remove("order1"), Times.Never);
        }
    }
}
