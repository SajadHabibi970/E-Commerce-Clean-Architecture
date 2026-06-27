using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Tests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void Constructor_ShouldCreateOrder_WhenDataIsValid()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var orderNumber = "ORD-001";
            var shippingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");
            var billingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");

            // Act
            var order = new Order(
                customerId,
                orderNumber,
                shippingAddress,
                billingAddress
            );

            // Assert
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(orderNumber, order.OrderNumber);
            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.Equal(0m, order.TotalAmount);
            Assert.Equal(shippingAddress, order.ShippingAddress);
            Assert.Equal(billingAddress, order.BillingAddress);
            Assert.NotEqual(Guid.Empty, order.Id);
            Assert.NotEqual(default, order.CreatedAt);
            Assert.Null(order.UpdatedAt);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenCustomerIdIsEmpty()
        {
            // Arrange
            var customerId = Guid.Empty;
            var orderNumber = "ORD-001";
            var shippingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");
            var billingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Order(
                customerId,
                orderNumber,
                shippingAddress,
                billingAddress
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenOrderNumberIsEmpty()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var orderNumber = "";
            var shippingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");
            var billingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Order(
                customerId,
                orderNumber,
                shippingAddress,
                billingAddress
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenShippingAddressIsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var orderNumber = "ORD-001";
            Address? shippingAddress = null;
            var billingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Order(
                customerId,
                orderNumber,
                shippingAddress,
                billingAddress
            ));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenBillingAddressIsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var orderNumber = "ORD-001";
            var shippingAddress = new Address("Storgatan 1", "Stockholm", "11122", "Sweden");
            Address? billingAddress = null;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            new Order(
                customerId,
                orderNumber,
                shippingAddress,
                billingAddress
            ));
        }

        [Fact]
        public void AddOrderItem_ShouldAddItemToOrder()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );
            var orderItem = new OrderItem(
                order.Id,
                Guid.NewGuid(),
                "Laptop",
                "ART-123",
                2,
                1000m
            );

            // Act
            order.AddOrderItem(orderItem);

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Contains(orderItem,order.OrderItems);
            Assert.Equal(2000m, order.TotalAmount);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void AddOrderItem_ShouldThrowException_WhenItemIsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            OrderItem? orderItem = null;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            order.AddOrderItem(orderItem!)
            );
        }

        [Fact]
        public void AddOrderItem_ShouldUpdateTotalAmount_WhenMultipleItemsAreAdded()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            var orderItem = new OrderItem(
                order.Id,
                Guid.NewGuid(),
                "Laptop", 
                "ART-123",
                2,
                1000m
            );
            var orderItem2 = new OrderItem(
                order.Id,
                Guid.NewGuid(),
                "Mouse",
                "ART-321",
                1,
                500m
            );

            // Act
            order.AddOrderItem(orderItem);
            order.AddOrderItem(orderItem2);

            // Assert
            Assert.Equal(2, order.OrderItems.Count);
            Assert.Contains(orderItem,order.OrderItems);
            Assert.Contains(orderItem2, order.OrderItems);
            Assert.Equal(2500m, order.TotalAmount);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void MarkAsPaid_ShouldChangeStatusToPaid()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.MarkAsPaid();

            // Assert
            Assert.Equal(OrderStatus.Paid, order.Status);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void MarkAsPaid_ShouldThrowException_WhenOrderIsNotPending()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act 
            order.MarkAsPaid();

            //Assert
            Assert.Throws<DomainException>(() =>
            order.MarkAsPaid());
        }

        [Fact]
        public void MarkAsProcessing_ShouldChangeStatusToProcessing()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.MarkAsPaid();
            order.MarkAsProcessing();

            // Assert
            Assert.Equal(OrderStatus.Processing, order.Status);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void MarkAsProcessing_ShouldThrowException_WhenOrderIsNotPaid()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            order.MarkAsProcessing());
        }

        [Fact]
        public void MarkAsShipped_ShouldChangeStatusToShipped()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.MarkAsPaid();
            order.MarkAsProcessing();
            order.MarkAsShipped();

            // Assert
            Assert.Equal(OrderStatus.Shipped, order.Status);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void MarkAsShipped_ShouldThrowException_WhenOrderIsNotProcessing()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            order.MarkAsShipped());
        }

        [Fact]
        public void MarkAsDelivered_ShouldChangeStatusToDelivered()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.MarkAsPaid();
            order.MarkAsProcessing();
            order.MarkAsShipped();
            order.MarkAsDelivered();

            // Assert
            Assert.Equal(OrderStatus.Delivered, order.Status);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void MarkAsDelivered_ShouldThrowException_WhenOrderIsNotShipped()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            order.MarkAsDelivered());
        }

        [Fact]
        public void Cancel_ShouldChangeStatusToCancelled()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Cancelled, order.Status);
            Assert.NotNull(order.UpdatedAt);
        }

        [Fact]
        public void Cancel_ShouldThrowException_WhenOrderIsDelivered()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.MarkAsPaid();
            order.MarkAsProcessing();
            order.MarkAsShipped();
            order.MarkAsDelivered();

            // Assert
            Assert.Throws<DomainException>(() =>
            order.Cancel());
        }

        [Fact]
        public void Cancel_ShouldThrowException_WhenOrderIsShipped()
        {
             // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.MarkAsPaid();
            order.MarkAsProcessing();
            order.MarkAsShipped();

            // Assert
            Assert.Throws<DomainException>(() =>
            order.Cancel());
        }

        [Fact]
        public void Cancel_ShouldThrowException_WhenOrderIsAlreadyCancelled()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(
                customerId,
                "ORD-001",
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden"),
                new Address("Storgatan 1", "Stockholm", "11122", "Sweden")
            );

            // Act
            order.Cancel();

            // Assert
            Assert.Throws<DomainException>(() =>
            order.Cancel());
        }
    }
}