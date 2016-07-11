using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

using Sample.Models.Mapping;

namespace Sample.Models
{
    /// <summary>
    /// This represents the database context entity for product.
    /// </summary>
    public class ProductDbContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ProductDbContext"/> class.
        /// </summary>
        public ProductDbContext()
            : base()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ProductDbContext"/> class.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{ProductDbContext}"/> instance.</param>
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Product}"/> instance.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{ProductPrice}"/> instance.
        /// </summary>
        public DbSet<ProductPrice> ProductPrices { get; set; }

        /// <summary>
        /// Called while entity models are created.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().Map(new ProductMap());
            builder.Entity<ProductPrice>().Map(new ProductPriceMap());
        }
    }
}
