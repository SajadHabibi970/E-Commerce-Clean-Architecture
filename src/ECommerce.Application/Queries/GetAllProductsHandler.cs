using ECommerce.Application.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Common;
using System.Linq;

namespace ECommerce.Application.Queries
{
    public sealed class GetAllProductHandler
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> HandleAsync(GetAllProductQuery query, CancellationToken ct = default)
        {
            var products = await _productRepository.GetAllAsync(ct);

            var dtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                ArticleNumber = product.ArticleNumber,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            }).ToList();

            return Result<IReadOnlyList<ProductDto>>.Success(dtos);
        }
    }
}