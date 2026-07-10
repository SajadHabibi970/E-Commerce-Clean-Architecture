using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.Description)
            .HasMaxLength(250);

            builder.Property(p => p.ArticleNumber)
            .IsRequired();

            builder.HasIndex(p => p.ArticleNumber)
            .IsUnique();

            builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);

            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(m => m.Amount)
                .HasColumnName("Price_Amount")
                .HasPrecision(18, 2);

                price.Property(m => m.Currency)
                .HasColumnName("Price_Currency")
                .HasMaxLength(3)
                .IsRequired();
            });

            builder.Property(p => p.StockQuantity)
            .IsRequired();

            builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}