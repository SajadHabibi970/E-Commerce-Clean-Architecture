using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Tests.Domain
{
    public class CartTests
    {
        [Fact]
        public void Constructor_ShouldCreateCart_WhenCustomerIdIsValid()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act 
            var cart = new Cart(
                customerId
            );

            // Assert
            Assert.Equal(customerId, cart.CustomerId);
            Assert.NotEqual(default, cart.CreatedAt);
            Assert.Null(cart.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenCustomerIdIsEmpty()
        {
            // Arrange
            var customerId = Guid.Empty;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Cart(
                customerId
            ));
        }
    }
}