using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliencube.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="EntityTypeBuilder{TEntity}"/> class.
    /// </summary>
    public static class EntityTypeBuilderExtenions
    {
        /// <summary>
        /// Maps entity model through entity type builder.
        /// </summary>
        /// <typeparam name="TEntity">Entity model class type.</typeparam>
        /// <typeparam name="TMapper">Entity model mapper type.</typeparam>
        /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> instance.</param>
        /// <param name="mapper"><see cref="IEntityMapper{TEntity}"/> instance.</param>
        public static EntityTypeBuilder<TEntity> Map<TEntity, TMapper>(this EntityTypeBuilder<TEntity> builder, TMapper mapper)
            where TEntity : class where TMapper : IEntityMapper<TEntity>
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            mapper.Map(builder);
            return builder;
        }

        /// <summary>
        /// Sets the default maximum length of string that is affected in the builder.
        /// </summary>
        /// <typeparam name="T">The entity type being configured.</typeparam>
        /// <param name="builder">The builder for the entity type being configured.</param>
        /// <param name="maxLength">The maximum length of data that is allowed in this property.</param>
        /// <returns>The builder for the entity type configured.</returns>
        public static EntityTypeBuilder<T> SetDefaultStringMaxLength<T>(this EntityTypeBuilder<T> builder, int? maxLength)
            where T : class
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Metadata
                   .GetProperties()
                   .Where(p => p.ClrType == typeof(string) && !p.GetMaxLength().HasValue)
                   .ToList()
                   .ForEach(p => { p.SetMaxLength(maxLength); });

            return builder;
        }
    }
}