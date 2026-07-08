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
            var trimmedName = name.Trim();

            EnsureValid(trimmedName);

            Id = Guid.NewGuid();
            Name = trimmedName;
            Description = description?.Trim();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
            IsActive = true;
        }

        private static void EnsureValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
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

            var trimmedName = name.Trim();

            EnsureValid(trimmedName);

            Name = trimmedName;
            Description = description?.Trim();
            UpdatedAt = DateTime.UtcNow;
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