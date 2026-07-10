using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.ValueObjects;

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

            try
            {
                var product = new Product(
                cmd.CategoryId,
                cmd.Name,
                cmd.Description,
                cmd.ArticleNumber,
                cmd.ImageUrl,
                new Money(cmd.Price, "SEK"),
                cmd.StockQuantity
                );

                await _productRepository.AddAsync(product, ct);
                return Result<Guid>.Success(product.Id);
            }
            
            catch(DomainException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }
    }
}