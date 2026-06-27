using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Tests.Domain
{
    public class CartItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateCartItem_WhenDataIsValid()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 10;
            var unitPrice = 1000m;

            // Act
            var cartItem = new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            );

            // Assert
            Assert.Equal(cartId, cartItem.CartId);
            Assert.Equal(productId, cartItem.ProductId);
            Assert.Equal(quantity, cartItem.Quantity);
            Assert.Equal(unitPrice, cartItem.UnitPrice);
            Assert.NotEqual(default, cartItem.CreatedAt);
            Assert.Null(cartItem.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenCartIdIsEmpty()
        {
            // Arrange
            var cartId = Guid.Empty;
            var productId = Guid.NewGuid();
            var quantity = 10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenProductIdIsEmpty()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.Empty;
            var quantity = 10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenQuantityIsZero()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 0;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenQuantityIsNegative()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = -10;
            var unitPrice = 1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenUnitPriceIsZero()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 10;
            var unitPrice = 0m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenUnitPriceIsNegative()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 10;
            var unitPrice = -1000m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new CartItem(
                cartId,
                productId,
                quantity,
                unitPrice
            ));
        }

        [Fact]
        public void ChangeQuantity_ShouldUpdateQuantity_WhenQuantityIsValid()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartItem = new CartItem(
                cartId,
                productId,
                10,
                1000m
            );

            // Act
            cartItem.ChangeQuantity(5);

            // Assert
            Assert.Equal(5, cartItem.Quantity);
            Assert.NotNull(cartItem.UpdatedAt);
        }

        [Fact]
        public void ChangeQuantity_ShouldThrowException_WhenQuantityIsZero()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartItem = new CartItem(
                cartId,
                productId,
                10,
                1000m
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            cartItem.ChangeQuantity(0)
            );    
        }

        [Fact]
        public void ChangeQuantity_ShouldThrowException_WhenQuantityIsNegative()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartItem = new CartItem(
                cartId,
                productId,
                10,
                1000m
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            cartItem.ChangeQuantity(-10)
            );    
        }
    }
}