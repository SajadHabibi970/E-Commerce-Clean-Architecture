using ECommerce.Application.Queries;
using ECommerce.Domain.Entities;
using ECommerce.Tests.Fakes;

namespace ECommerce.Tests.Application.Products
{
    public class GetAllProductsHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnSuccess_WithEmptyList_WithNoProductsExists()
        {
            // Arrange
            var repository = new FakeProductRepository();

            var handler = new GetAllProductsHandler(repository);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnSuccess_WithProducts_WhenProductsExist()
        {
            // Arrange
            var repository = new FakeProductRepository();
            
            var product = new Product(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                1000m,
                10
            );
            repository.Products.Add(product);

            var product2 = new Product(
                Guid.NewGuid(),
                "Mouse",
                "Gaming mouse",
                "ART-002",
                "mouse.jpg",
                100m,
                15
            );
            repository.Products.Add(product2);

            var handler = new GetAllProductsHandler(repository);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(2, result.Value!.Count);
            Assert.Equal("Laptop", result.Value[0].Name);
            Assert.Equal("Mouse", result.Value[1].Name);
        }
    }
}