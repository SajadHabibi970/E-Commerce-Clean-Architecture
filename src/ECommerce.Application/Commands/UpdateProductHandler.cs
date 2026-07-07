using ECommerce.Application.Interfaces;
using ECommerce.Application.Common;

namespace ECommerce.Application.Commands
{
    public sealed class UpdateProductHandler
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<Guid>> HandleAsync(UpdateProductCommand cmd, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(cmd);

            if (cmd.Id == Guid.Empty)
            {
                return Result<Guid>.Failure("Product id cannot be empty");
            }

            var product = await _productRepository.GetByIdAsync(cmd.Id, ct);

                if (product is null)
                {
                    return Result<Guid>.NotFound("Product not found");
                }

                product.Edit(
                    cmd.CategoryId,
                    cmd.Name,
                    cmd.Description,
                    cmd.ArticleNumber,
                    cmd.ImageUrl,
                    cmd.Price,
                    cmd.StockQuantity
                );

                await _productRepository.UpdateAsync(product, ct);

                return Result<Guid>.Success(product.Id);
            
        }
    }
}