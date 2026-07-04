using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Products
            .ToListAsync(ct);
        }

        public async Task AddAsync(Product product, CancellationToken ct = default)
        {
            await _context.Products.AddAsync(product, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(ct);
        }
    }
}