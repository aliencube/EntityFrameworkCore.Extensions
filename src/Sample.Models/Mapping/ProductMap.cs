using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Models.Mapping
{
    /// <summary>
    /// This represents the mapper entity for the <see cref="Product"/> class.
    /// </summary>
    public class ProductMap : IEntityMapper<Product>
    {
        /// <summary>
        /// Maps database entity to entity model.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{Product}"/> instance.</param>
        public void Map(EntityTypeBuilder<Product> builder)
        {
            // Primary Key
            builder.HasKey(p => p.ProductId);

            // Properties
            builder.Property(p => p.ProductId).IsRequired().UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(64);

            // Table & Column Mappings
            builder.ToTable("Product");
            builder.Property(p => p.ProductId).HasColumnName("ProductId");
            builder.Property(p => p.Name).HasColumnName("Name");
        }
    }
}
