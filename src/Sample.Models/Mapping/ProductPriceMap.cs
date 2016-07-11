using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Models.Mapping
{
    /// <summary>
    /// This represents the mapper entity for the <see cref="ProductPrice"/> class.
    /// </summary>
    public class ProductPriceMap : IEntityMapper<ProductPrice>
    {
        /// <summary>
        /// Maps database entity to entity model.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{ProductPrice}"/> instance.</param>
        public void Map(EntityTypeBuilder<ProductPrice> builder)
        {
            // Primary Key
            builder.HasKey(p => p.ProductPriceId);

            // Properties
            builder.Property(p => p.ProductPriceId).IsRequired().UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.ValidFrom).IsRequired();
            builder.Property(p => p.ValidTo).IsRequired(false);

            // Table & Column Mappings
            builder.ToTable("ProductPrice");
            builder.Property(p => p.ProductPriceId).HasColumnName("ProductPriceId");
            builder.Property(p => p.ProductId).HasColumnName("ProductId");
            builder.Property(p => p.Value).HasColumnName("Value");
            builder.Property(p => p.ValidFrom).HasColumnName("ValidFrom");
            builder.Property(p => p.ValidTo).HasColumnName("ValidTo");

            // Relationships
            builder.HasOne(p => p.Product)
                   .WithMany(p => p.ProductPrices)
                   .HasForeignKey(p => p.ProductId);
        }
    }
}