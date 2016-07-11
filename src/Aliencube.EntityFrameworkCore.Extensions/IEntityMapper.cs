using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliencube.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// This provides interface to entity mapper classes.
    /// </summary>
    /// <typeparam name="TEntity">Entity model class type.</typeparam>
    public interface IEntityMapper<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Maps database entity to entity model.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/> instance.</param>
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}