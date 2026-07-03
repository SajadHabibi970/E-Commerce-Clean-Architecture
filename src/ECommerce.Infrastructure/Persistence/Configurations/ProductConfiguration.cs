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

            builder.Property(p => p.Price)
            .HasPrecision(18, 2);

            builder.Property(p => p.StockQuantity)
            .IsRequired();

            builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);
        }
    }
}