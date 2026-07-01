using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Commands
{
    public sealed class CreateProductHandler
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<Guid>> HandleAsync(CreateProductCommand cmd, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(cmd);
            
            var product = new Product(
                cmd.CategoryId,
                cmd.Name,
                cmd.Description,
                cmd.ArticleNumber,
                cmd.ImageUrl,
                cmd.Price,
                cmd.StockQuantity
            );

            await _productRepository.AddAsync(product, ct);
            return Result<Guid>.Success(product.Id);
        }
    }
}