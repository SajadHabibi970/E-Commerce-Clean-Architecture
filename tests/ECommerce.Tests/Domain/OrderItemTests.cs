using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Tests.Domain
{
    public class OrderItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateOrderItem_WhenDataIsValid()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = 10;
            var unitPrice = 1000m;

            // Act
            var orderItem = new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
                
            );

            // Assert
            Assert.Equal(orderId, orderItem.OrderId);
            Assert.Equal(productId, orderItem.ProductId);
            Assert.Equal(productName, orderItem.ProductName);
            Assert.Equal(articleNumber, orderItem.ArticleNumber);
            Assert.Equal(quantity, orderItem.Quantity);
            Assert.Equal(unitPrice, orderItem.UnitPrice);
            Assert.Equal(quantity * unitPrice, orderItem.TotalPrice);
            Assert.NotEqual(default, orderItem.CreatedAt);
            Assert.Null(orderItem.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenOrderIdIsEmpty()
        {
            // Arrange
            var orderId = Guid.Empty;
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = 10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenProductIdIsEmpty()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.Empty;
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = 10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenProductNameIsEmpty()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "";
            var articleNumber = "ART-001";
            var quantity = 10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenArticleNumberIsEmpty()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "";
            var quantity = 10;
            var unitPrice = 1000;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenQuantityIsZero()
        {
            // Arrange 
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = 0;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenQuantityIsNegative()
        {
            // Arrange 
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = -10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() => 
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenUnitPriceIsZero()
        {
            // Arrange 
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = 10;
            var unitPrice = 0;

            // Act & Assert
            Assert.Throws<DomainException>(() => 
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenUnitPriceIsNegative()
        {
            // Arrange 
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Laptop";
            var articleNumber = "ART-001";
            var quantity = 10;
            var unitPrice = -1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() => 
            new OrderItem(
                orderId,
                productId,
                productName,
                articleNumber,
                quantity,
                unitPrice
            ));
        }
    }
}