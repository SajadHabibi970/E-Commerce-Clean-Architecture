using ECommerce.Application.Commands;
using ECommerce.Domain.Entities;
using ECommerce.Tests.Fakes;

namespace ECommerce.Tests.Application.Products
{
    public class UpdateProductHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldUpdateProduct_WhenProductExists()
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

            var handler = new UpdateProductHandler(repository);
            var cmd = new UpdateProductCommand(
                product.Id,
                product.CategoryId,
                "Mouse",
                "Gaming mouse",
                "ART-002",
                "mouse.jpg",
                100m,
                10
            );

            // Act
            var result = await handler.HandleAsync(cmd);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(product.Id, result.Value);
            Assert.Equal("Mouse",product.Name);
            Assert.Equal("Gaming mouse", product.Description);
            Assert.Equal("ART-002",product.ArticleNumber);
            Assert.Equal("mouse.jpg", product.ImageUrl);
            Assert.Equal(100m, product.Price);
            Assert.Equal(10, product.StockQuantity);
            Assert.NotNull(product.UpdatedAt);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFailure_WhenIdIsEmpty()
        {
            // Arrange
            var repository = new FakeProductRepository();
            
            var handler = new UpdateProductHandler(repository);
            var cmd = new UpdateProductCommand(
                Guid.Empty,
                Guid.NewGuid(),
                "Mouse",
                "Gaming mouse",
                "ART-002",
                "mouse.jpg",
                100m,
                10
            );

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

            var handler = new UpdateProductHandler(repository);
            var cmd = new UpdateProductCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Mouse",
                "Gaming mouse",
                "ART-002",
                "mouse.jpg",
                100m,
                10
            );

            // Act
            var result = await handler.HandleAsync(cmd);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product not found", result.Error);
        }
    }
}