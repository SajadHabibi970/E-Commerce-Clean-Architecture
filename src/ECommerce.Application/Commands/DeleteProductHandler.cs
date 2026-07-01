using ECommerce.Application.Interfaces;
using ECommerce.Application.Common;

namespace ECommerce.Application.Commands
{
    public sealed class DeleteProductHandler
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<Guid>> HandleAsync(DeleteProductCommand cmd, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(cmd);

            if (cmd.Id == Guid.Empty)
            {
                return Result<Guid>.Failure("Product id cannot be empty");
            }

            var product = await _productRepository.GetByIdAsync(cmd.Id, ct);

            if (product is null)
            {
                return Result<Guid>.Failure("Product not found");
            }

            product.SoftDelete();

            await _productRepository.UpdateAsync(product, ct);

            return Result<Guid>.Success(product.Id);
        }
    }
}