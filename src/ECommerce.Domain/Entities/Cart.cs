namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public List<CartItem> CartItems { get; private set; } = new();

        public Cart (Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (CustomerId == Guid.Empty)
            {
                throw new DomainException("CustomerId is required");
            }
        }
    }
}