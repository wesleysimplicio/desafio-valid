using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;
using Xunit;

namespace ValidKitchenControl.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly Mock<IOrderRepository> _mockOrderRepository;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockOrderService = new Mock<IOrderService>();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCallAddAsync()
        {
            // Arrange
            var order = new Order { Id = 1, Item = "Batata Frita", Quantity = 2, Area = "fritos" };

            _mockOrderService.Setup(service => service.CreateOrderAsync(order))
                .Returns(async () =>
                {
                    await _mockOrderRepository.Object.AddAsync(order);
                });

            // Act
            await _mockOrderService.Object.CreateOrderAsync(order);

            // Assert
            _mockOrderRepository.Verify(repo => repo.AddAsync(order), Times.Once);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, Item = "Batata Frita", Quantity = 2, Area = "fritos" },
                new Order { Id = 2, Item = "Hambúrguer", Quantity = 1, Area = "grelhados" }
            };

            _mockOrderRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

            _mockOrderService.Setup(service => service.GetAllOrdersAsync())
                .Returns(async () => await _mockOrderRepository.Object.GetAllAsync());

            // Act
            var result = await _mockOrderService.Object.GetAllOrdersAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Id == 1);
            Assert.Contains(result, p => p.Id == 2);
        }
    }
}
