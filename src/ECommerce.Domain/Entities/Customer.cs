using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? PhoneNumber { get; private set; }
        public string? Address { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public Customer(string firstName, string lastName, string email, string? phoneNumber, string? address)
        {
            Id = Guid.NewGuid();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber?.Trim();
            Address = address?.Trim();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                throw new DomainException("FirstName cannot be empty");
            }
            
            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new DomainException("LastName cannot be empty");
            }
            
            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new DomainException("Email cannot be empty");
            }

            if (!Email.Contains("@"))
            {
                throw new DomainException("Email must contain '@'");
            }
        }

        public void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException("The customer is already deleted");
            }
        }

        public void Activate()
        {
            EnsureNotDeleted();

            if (IsActive)
            {
                throw new DomainException("Customer is already active");
            }

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            EnsureNotDeleted();

            if (!IsActive)
            {
                throw new DomainException("Customer is already inactive");
            }

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Edit(string firstName, string lastName, string email, string? phoneNumber, string? address)
        {
            EnsureNotDeleted();

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber?.Trim();
            Address = address?.Trim();

            UpdatedAt = DateTime.UtcNow;

            EnsureValid();
        }

        public void SoftDelete()
        {
            EnsureNotDeleted();

            IsActive = false;
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = DateTime.UtcNow;
        }
    }
}