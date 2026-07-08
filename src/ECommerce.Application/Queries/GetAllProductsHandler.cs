using ECommerce.Application.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.Application.Common;
using System.Linq;

namespace ECommerce.Application.Queries
{
    public sealed class GetAllProductsHandler
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> HandleAsync(GetAllProductsQuery query, CancellationToken ct = default)
        {
            var products = await _productRepository.GetAllAsync(ct);

            var dtos = products.Select(product => product.ToDto()).ToList();

            return Result<IReadOnlyList<ProductDto>>.Success(dtos);
        }
    }
}