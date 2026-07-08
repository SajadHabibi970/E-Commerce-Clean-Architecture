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
            var trimmedProductName = productName.Trim();
            var trimmedArticleNumber = articleNumber.Trim();

            EnsureValid(orderId, productId, trimmedProductName, trimmedArticleNumber, quantity, unitPrice);

            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            ProductName = trimmedProductName;
            ArticleNumber = trimmedArticleNumber;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }

        private static void EnsureValid(Guid orderId, Guid productId, string productName, string articleNumber, int quantity, decimal unitPrice)
        {
            if (orderId == Guid.Empty)
            {
                throw new DomainException("OrderId is required");
            }

            if (productId == Guid.Empty)
            {
                throw new DomainException("ProductId is required");
            }

            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new DomainException("ProductName cannot be empty");
            }
            
            if (string.IsNullOrWhiteSpace(articleNumber))
            {
                throw new DomainException("ArticleNumber cannot be empty");
            }

            if (quantity <= 0)
            {
                throw new DomainException("Quantity must be greater than 0");
            }

            if (unitPrice <= 0)
            {
                throw new DomainException("UnitPrice must be greater than 0");
            }
        }
    }
}