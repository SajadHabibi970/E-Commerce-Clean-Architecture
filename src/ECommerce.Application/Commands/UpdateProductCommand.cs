namespace ECommerce.Application.Commands
{
    public sealed record UpdateProductCommand(
        Guid Id,
        Guid CategoryId,
        string Name,
        string? Description,
        string ArticleNumber,
        string ImageUrl,
        decimal Price,
        int StockQuantity
    );
}