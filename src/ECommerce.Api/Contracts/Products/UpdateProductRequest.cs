namespace ECommerce.Api.Contracts.Products
{
    public sealed record UpdateProductRequest(
        Guid CategoryId,
        string Name,
        string? Description,
        string ArticleNumber,
        string ImageUrl,
        decimal Price,
        int StockQuantity
    );
}