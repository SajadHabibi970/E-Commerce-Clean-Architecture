using ECommerce.Application.Queries;
using ECommerce.Domain.Entities;
using ECommerce.Tests.Fakes;

namespace ECommerce.Tests.Application.Products
{
    public class GetProductByIdHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnSuccess_WhenProductExists()
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

            var handler = new GetProductByIdHandler(repository);
            var query = new GetProductByIdQuery(product.Id);

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(product.Id, result.Value.Id);
            Assert.Equal(product.Name, result.Value.Name);
            Assert.Equal(product.Price, result.Value.Price);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            // Arrange
            var repository = new FakeProductRepository();

            var handler = new GetProductByIdHandler(repository);
            var query = new GetProductByIdQuery(Guid.NewGuid());

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product not found", result.Error);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFailure_WhenIdIsEmpty()
        {
            // Arrange
            var repository = new FakeProductRepository();
            
            var handler = new GetProductByIdHandler(repository);
            var query = new GetProductByIdQuery(Guid.Empty);

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product id cannot be empty", result.Error);
        }
    }
}