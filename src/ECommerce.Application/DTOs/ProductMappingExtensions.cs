using ECommerce.Domain.Entities;

namespace ECommerce.Application.DTOs
{
    public static class ProductMappingExtensions
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                ArticleNumber = product.ArticleNumber,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }
    }
}