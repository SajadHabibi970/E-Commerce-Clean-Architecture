using ECommerce.Application.Commands;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects;
using ECommerce.Tests.Fakes;

namespace ECommerce.Tests.Application.Products
{
    public class DeleteProductHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldSoftDeleteProduct_WhenProductExists()
        {
            // Arrange
            var repository = new FakeProductRepository();

            var product = new Product(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                new Money(1000m, "SEK"),
                10
            );
            repository.Products.Add(product);

            var handler = new DeleteProductHandler(repository);
            var cmd = new DeleteProductCommand(product.Id);

            // Act
            var result = await handler.HandleAsync(cmd);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(product.IsDeleted);
            Assert.Equal(product.Id, result.Value);
            Assert.NotNull(product.DeletedAt);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFailure_WhenIdIsEmpty()
        {
            // Arrange
            var repository = new FakeProductRepository();
            
            var handler = new DeleteProductHandler(repository);
            var cmd = new DeleteProductCommand(Guid.Empty);

            // Act
            var result = await handler.HandleAsync(cmd);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product id cannot be empty", result.Error);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFailure_WhenProductDoesNotExist()
        {
            // Arrange
            var repository = new FakeProductRepository();

            var handler = new DeleteProductHandler(repository);
            var cmd = new DeleteProductCommand(Guid.NewGuid());

            // Act
            var result = await handler.HandleAsync(cmd);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product not found", result.Error);
        }
    }

}