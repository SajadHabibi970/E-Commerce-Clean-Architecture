namespace ECommerce.Domain.Exceptions
{
    public class InvalidOrderStatusException : DomainException
    {
        public InvalidOrderStatusException(string? message) : base(message) {}
    }
}