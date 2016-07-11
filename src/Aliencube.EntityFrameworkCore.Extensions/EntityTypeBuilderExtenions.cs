using System;

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
    }
}