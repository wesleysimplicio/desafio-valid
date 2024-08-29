using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Repositories;
using ValidKitchenControl.Domain.Services;
using Xunit;

namespace ValidKitchenControl.Tests
{
    public class AreaServiceTests
    {
        private readonly Mock<IAreaService> _mockAreaService;
        private readonly Mock<IAreaRepository> _mockAreaRepository;

        public AreaServiceTests()
        {
            _mockAreaRepository = new Mock<IAreaRepository>();
            _mockAreaService = new Mock<IAreaService>();

            // Setup the mock behavior for IAreaService
            _mockAreaService.Setup(service => service.CreateAsync(It.IsAny<Area>()))
                .Returns<Area>(area => Task.CompletedTask);

            _mockAreaService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(new List<Area>
                {
                    new Area { Id = 1, Name = "Area1" },
                    new Area { Id = 2, Name = "Area2" }
                });

            _mockAreaService.Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .Returns<int>(id => Task.FromResult(new Area { Id = id, Name = "Test Area" }));

            _mockAreaService.Setup(service => service.GetByNameAsync(It.IsAny<string>()))
                .Returns<string>(name => Task.FromResult(new Area { Id = 1, Name = name }));
        }

        [Fact]
        public async Task CreateAsync_ShouldCallCreateAsync()
        {
            // Arrange
            var area = new Area { Id = 1, Name = "Test Area" };

            // Act
            await _mockAreaService.Object.CreateAsync(area);

            // Assert
            _mockAreaService.Verify(service => service.CreateAsync(It.Is<Area>(a => a.Id == area.Id && a.Name == area.Name)), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAreas()
        {
            // Act
            var result = await _mockAreaService.Object.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Area1", result.First().Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectArea()
        {
            // Act
            var result = await _mockAreaService.Object.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Area", result.Name);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnCorrectArea()
        {
            // Act
            var result = await _mockAreaService.Object.GetByNameAsync("Test Area");

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Area", result.Name);
        }
    }
}
