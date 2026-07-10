using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Tests.Infrastructure
{
    public class ProductRepositoryTests
    {
        private ECommerceDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            return new ECommerceDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveProduct()
        {
            // Arrange
            using var context = CreateDbContext();

            var repository = new ProductRepository(context);

            var product = new Product(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                new Money(1000m, "SEK"),
                10
            );

            // Act
            await repository.AddAsync(product);

            // Assert
            var savedProduct = await context.Products
            .FirstOrDefaultAsync(p => p.Id == product.Id);

            Assert.NotNull(savedProduct);
            Assert.Equal(product.Id, savedProduct.Id);
            Assert.Equal(product.Name, savedProduct.Name);
            Assert.Equal(product.Price, savedProduct.Price);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            using var context = CreateDbContext();

            var repository = new ProductRepository(context);

            var product = new Product(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                new Money(1000m, "SEK"),
                10
            );

            await repository.AddAsync(product);

            // Act
            var result = await repository.GetByIdAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            using var context = CreateDbContext();
            
            var repository = new ProductRepository(context);

            // Act 
            var result = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProducts_WhenProductsExist()
        {
            // Arrange
            using var context = CreateDbContext();

            var repository = new ProductRepository(context);

            var product = new Product(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                new Money(1000m, "SEK"),
                10
            );
            
            var product2 = new Product(
                Guid.NewGuid(),
                "Mouse",
                "Gaming mouse",
                "ART-002",
                "mouse.jpg",
                new Money(100m, "SEK"),
                10
            );

            await repository.AddAsync(product);
            await repository.AddAsync(product2);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Laptop");
            Assert.Contains(result, p => p.Name == "Mouse");
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            using var context = CreateDbContext();

            var repository = new ProductRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            using var context = CreateDbContext();

            var repository = new ProductRepository(context);

            var product = new Product(
                Guid.NewGuid(),
                "Laptop",
                "Gaming laptop",
                "ART-001",
                "laptop.jpg",
                new Money(1000m, "SEK"),
                10
            );
            await repository.AddAsync(product);


            // Act
            product.Edit(
                product.CategoryId,
                "Mouse",
                "Gaming mouse",
                "ART-002",
                "mouse.jpg",
                new Money(100m, "SEK"),
                10
            );
            await repository.UpdateAsync(product);

            // Assert
            var updatedProduct = await repository.GetByIdAsync(product.Id);

            Assert.NotNull(updatedProduct);
            Assert.Equal("Mouse", updatedProduct.Name);
            Assert.Equal("Gaming mouse", updatedProduct.Description);
            Assert.Equal("ART-002", updatedProduct.ArticleNumber);
            Assert.Equal("mouse.jpg", updatedProduct.ImageUrl);
            Assert.Equal(100m, updatedProduct.Price.Amount);
            Assert.Equal(10, updatedProduct.StockQuantity);
            Assert.NotNull(updatedProduct.UpdatedAt);
        }
    }
}
