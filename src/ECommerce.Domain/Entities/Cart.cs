namespace ECommerce.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public List<CartItem> CartItems { get; private set; } = new();
    }
}