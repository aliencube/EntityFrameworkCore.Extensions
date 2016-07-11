using System;

namespace Sample.Models
{
    /// <summary>
    /// This represents the model entity for the product price.
    /// </summary>
    public class ProductPrice
    {
        /// <summary>
        /// Gets or sets the product price Id.
        /// </summary>
        public int ProductPriceId { get; set; }

        /// <summary>
        /// Gets or sets the product Id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product price value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the starting date and time when the price is valid.
        /// </summary>
        public DateTimeOffset ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the ending date and time until the price is valid.
        /// </summary>
        public DateTimeOffset? ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Product"/> instance.
        /// </summary>
        public Product Product { get; set; }
    }
}