namespace ECommerce.Api.Contracts.Products
{
    public sealed record CreateProductRequest(
        Guid CategoryId,
        string Name,
        string? Description,
        string ArticleNumber,
        string ImageUrl,
        decimal Price,
        int StockQuantity
    );
}