using ECommerce.Application.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Common;

namespace ECommerce.Application.Queries
{
    public sealed class GetProductByIdHandler
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<ProductDto>> HandleAsync(GetProductByIdQuery query, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            
            if (query.Id == Guid.Empty)
            {
                return Result<ProductDto>.Failure("Product id cannot be empty");
            }

            var product = await _productRepository.GetByIdAsync(query.Id, ct);

            if (product == null)
            {
                return Result<ProductDto>.NotFound("Product not found");
            }

            var dto = product.ToDto();

            return Result<ProductDto>.Success(dto);
        }
    }
}