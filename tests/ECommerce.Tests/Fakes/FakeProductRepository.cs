using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Tests.Fakes
{
    public class FakeProductRepository : IProductRepository
    {
        public List<Product> Products { get; } = new();

        public Task AddAsync(Product product, CancellationToken ct = default)
        {
            Products.Add(product);
            return Task.CompletedTask;
        }

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return Task.FromResult<IReadOnlyList<Product>>(Products);
        }

        public Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            return Task.CompletedTask;
        }
    }
}