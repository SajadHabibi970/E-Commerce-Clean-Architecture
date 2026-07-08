using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.ValueObjects
{
    public sealed record Money(decimal Amount, string Currency)
    {
        public decimal Amount { get; init; } = Amount >= 0
        ? Amount
        : throw new DomainException("Amount cannot be negative");

        public string Currency { get; init; } = !string.IsNullOrWhiteSpace(Currency)
        ? Currency
        : throw new DomainException("Currency is required");
    }
}