using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string OrderNumber { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public List<OrderItem> OrderItems { get; private set; } = new();

        public Order (Guid customerId, string orderNumber, Address shippingAddress, Address billingAddress)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderNumber = orderNumber.Trim();
            Status = OrderStatus.Pending;
            TotalAmount = 0;
            PaymentStatus = PaymentStatus.Pending;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (CustomerId == Guid.Empty)
            {
                throw new DomainException("CustomerId cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(OrderNumber))
            {
                throw new DomainException("OrderNumber cannot be empty");
            }

            if (ShippingAddress == null)
            {
                throw new DomainException("ShippingAddress cannot be null");
            }

            if (BillingAddress == null)
            {
                throw new DomainException("BillingAddress cannot be null");
            }
        }

        public void AddOrderItem(OrderItem item, Product product)
        {
            if(item == null)
            {
                throw new DomainException("OrderItem cannot be null");
            }

            product.ReduceStock(item.Quantity);
            OrderItems.Add(item);
            CalculateTotalAmount();
            UpdatedAt = DateTime.UtcNow;
        }  

        private void CalculateTotalAmount()
        {
            TotalAmount = OrderItems.Sum(item => item.TotalPrice);
        }

        public void MarkAsPaid()
        {
            if (PaymentStatus != PaymentStatus.Pending)
            {
                throw new InvalidOrderStatusException("Only pending orders can be marked as paid.");
            }

            PaymentStatus = PaymentStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsProcessing()
        {
            if (PaymentStatus != PaymentStatus.Paid)
            {
                throw new InvalidOrderStatusException("Only paid orders can be marked as processing");
            }

            Status = OrderStatus.Processing;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsShipped()
        {
            if (Status != OrderStatus.Processing)
            {
                throw new InvalidOrderStatusException("Only processing orders can be marked as shipped");
            }

            Status = OrderStatus.Shipped;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDelivered()
        {
            if (Status != OrderStatus.Shipped)
            {
                throw new InvalidOrderStatusException("Only shipped orders can be marked as delivered");
            }

            Status = OrderStatus.Delivered;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsRefunded()
        {
            if (PaymentStatus != PaymentStatus.Paid)
            {
                throw new InvalidOrderStatusException("Cannot refund something that has not been paid");
            }

            PaymentStatus = PaymentStatus.Refunded;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Delivered || Status == OrderStatus.Shipped || Status == OrderStatus.Cancelled)
            {
                throw new InvalidOrderStatusException("This order can no longer be cancelled");
            }

            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}