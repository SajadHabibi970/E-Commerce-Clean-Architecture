using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public List<Product> Products { get; private set; } = new();

        public Category(string name, string? description)
        {
            Id = Guid.NewGuid();
            Name = name?.Trim();
            Description = description?.Trim();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
            IsActive = true;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Category name cannot be empty");
            }
        }

        public void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException("The category is already deleted.");
            }
        }

        public void Activate()
        {
            EnsureNotDeleted();
            if(IsActive)
            {
                throw new DomainException("The category is already active.");
            }

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            EnsureNotDeleted();

            if (!IsActive)
            {
                throw new DomainException("The category is already inactive.");
            }

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Edit(string name, string? description)
        {
            EnsureNotDeleted();

            Name = name?.Trim();
            Description = description?.Trim();
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