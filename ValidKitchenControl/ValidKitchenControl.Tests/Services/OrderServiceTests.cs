using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidKitchenControl.Application.Services;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;
using Xunit;

namespace ValidKitchenControl.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IAreaService> _areaServiceMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _areaServiceMock = new Mock<IAreaService>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _areaServiceMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenAreaIsInvalid()
        {
            // Arrange
            var order = new Order { Area = "InvalidArea" };
            _areaServiceMock.Setup(a => a.GetByNameAsync(order.Area)).ReturnsAsync((Area)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.CreateAsync(order));
            Assert.Equal("Area inválida", exception.Message); // Ajuste a mensagem de exceção conforme sua implementação
        }

        [Fact]
        public async Task CreateAsync_ShouldAddOrder_WhenAreaIsValid()
        {
            // Arrange
            var order = new Order { Area = "ValidArea" };
            var area = new Area { Name = "ValidArea" };
            _areaServiceMock.Setup(a => a.GetByNameAsync(order.Area)).ReturnsAsync(area);
            _orderRepositoryMock.Setup(o => o.AddAsync(order)).Returns(Task.CompletedTask);

            // Act
            await _orderService.CreateAsync(order);

            // Assert
            _orderRepositoryMock.Verify(o => o.AddAsync(order), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order> { new Order(), new Order() };
            _orderRepositoryMock.Setup(o => o.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var order = new Order { Id = 1 };
            _orderRepositoryMock.Setup(o => o.GetByIdAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateOrder()
        {
            // Arrange
            var order = new Order { Id = 1, Area = "UpdatedArea" };
            _orderRepositoryMock.Setup(o => o.UpdateAsync(order)).Returns(Task.CompletedTask);

            // Act
            await _orderService.UpdateAsync(order);

            // Assert
            _orderRepositoryMock.Verify(o => o.UpdateAsync(order), Times.Once);
        }
    }
}
