using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.ValueObjects
{
    public sealed record Address(string Street, string City, string PostalCode, string Country)
    {
        public string Street { get; init; } = !string.IsNullOrWhiteSpace(Street)
        ? Street
        : throw new DomainException("Street is required");

        public string City { get; init; } = !string.IsNullOrWhiteSpace(City)
        ? City
        : throw new DomainException("City is required");

        public string PostalCode { get; init; } = !string.IsNullOrWhiteSpace(PostalCode)
        ? PostalCode
        : throw new DomainException("PostalCode is required");

        public string Country { get; init; } = !string.IsNullOrWhiteSpace(Country)
        ? Country
        : throw new DomainException("Country is required");
    }
}