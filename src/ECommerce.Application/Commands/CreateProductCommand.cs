namespace ECommerce.Application.Commands
{
    public sealed record CreateProductCommand(
        Guid CategoryId,
        string Name,
        string? Description,
        string ArticleNumber,
        string ImageUrl,
        decimal Price,
        int StockQuantity
    );
}