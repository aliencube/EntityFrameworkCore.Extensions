using System.Collections.Generic;

namespace Sample.Models
{
    /// <summary>
    /// This represents the model entity for product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.ProductPrices = new List<ProductPrice>();
        }

        /// <summary>
        /// Gets or sets the product Id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of the <see cref="ProductPrice"/> instances.
        /// </summary>
        public List<ProductPrice> ProductPrices { get; set; }
    }
}