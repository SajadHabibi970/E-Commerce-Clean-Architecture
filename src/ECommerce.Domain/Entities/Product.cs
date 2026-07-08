using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities
{
    public class Product
    {
    public Guid Id { get; private set; }
    public string Name { get; private set;} = null!;
    public string? Description { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ImageUrl { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set;}
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public Product (Guid categoryId, string name, string? description, string articleNumber, string imageUrl, decimal price, int stockQuantity)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Name = name.Trim();
            Description = description?.Trim();
            ArticleNumber = articleNumber.Trim();
            ImageUrl = imageUrl.Trim();
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;

            EnsureValid();
        }

        private void EnsureValid()
        {
            if (CategoryId == Guid.Empty)
            {
                throw new DomainException("CategoryId is required");
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Product name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(ArticleNumber))
            {
                throw new DomainException("ArticleNumber cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(ImageUrl))
            {
                throw new DomainException("ImageUrl is required");
            }

            if (Price <= 0)
            {
                throw new DomainException("Price must be greater than 0");
            }

            if (StockQuantity < 0)
            {
                throw new DomainException("StockQuantity cannot be negative");
            }
        }

        public void Activate()
        {
            EnsureNotDeleted();

            if (IsActive)
            {
                throw new DomainException("Product is already active");
            }

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            EnsureNotDeleted();

            if (!IsActive)
            {
                throw new DomainException("Product is already inactive");
            }

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException("The product is already deleted");
            }
        }

        public void Edit(Guid categoryId, string name, string? description, string articleNumber, string imageUrl, decimal price, int stockQuantity)
        {
            EnsureNotDeleted();

            CategoryId = categoryId;
            Name = name.Trim();
            Description = description?.Trim();
            ArticleNumber = articleNumber.Trim();
            ImageUrl = imageUrl.Trim();
            Price = price;
            StockQuantity = stockQuantity;
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