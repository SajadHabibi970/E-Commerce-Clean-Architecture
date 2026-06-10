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
    public Guid CategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    }
}