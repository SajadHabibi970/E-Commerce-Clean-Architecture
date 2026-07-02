using ECommerce.Application.Commands;
using ECommerce.Tests.Fakes;

namespace ECommerce.Tests.Application.Products
{
    public class CreateProductHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnSuccess_WhenCommandIsValid()
        {
            // Arrange
            var repository = new FakeProductRepository();
            var handler = new CreateProductHandler(repository);

            var command = new CreateProductCommand(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                1000m,
                10
            );

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);
            Assert.Single(repository.Products);
        }
    }
}