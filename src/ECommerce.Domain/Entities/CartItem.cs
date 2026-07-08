using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; private set; }
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public CartItem (Guid cartId, Guid productId, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (CartId == Guid.Empty)
            {
                throw new DomainException("CartId is required");
            }

            if (ProductId == Guid.Empty)
            {
                throw new DomainException("ProductId is required");
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

        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Quantity must be greater than 0");
            }

            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}