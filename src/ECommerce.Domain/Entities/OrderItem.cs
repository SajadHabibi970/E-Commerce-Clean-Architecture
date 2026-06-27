using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ArticleNumber { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public OrderItem(Guid orderId, Guid productId, string productName, string articleNumber, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName?.Trim();
            ArticleNumber = articleNumber?.Trim();
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (OrderId == Guid.Empty)
            {
                throw new DomainException("OrderId is required");
            }

            if (ProductId == Guid.Empty)
            {
                throw new DomainException("ProductId is required");
            }

            if (string.IsNullOrWhiteSpace(ProductName))
            {
                throw new DomainException("ProductName cannot be empty");
            }
            
            if (string.IsNullOrWhiteSpace(ArticleNumber))
            {
                throw new DomainException("ArticleNumber cannot be empty");
            }

            if (Quantity <= 0)
            {
                throw new DomainException("Quantity must be greater than 0");
            }

            if (UnitPrice <= 0)
            {
                throw new DomainException("UnitPrice must be greater than 0");
            }
        }
    }
}