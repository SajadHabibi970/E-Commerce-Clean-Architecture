using ECommerce.Application.Interfaces;
using ECommerce.Application.Common;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.ValueObjects;

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

            try
            {
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
                    new Money(cmd.Price, "SEK"),
                    cmd.StockQuantity
                );

                await _productRepository.UpdateAsync(product, ct);

                return Result<Guid>.Success(product.Id);
            }

            catch(DomainException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
            
        }
    }
}