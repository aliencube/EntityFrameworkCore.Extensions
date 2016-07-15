using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures
{
    /// <summary>
    /// This represents the mapper entity for Foo.
    /// </summary>
    public class FooMapper : IEntityMapper<Foo>
    {
        /// <summary>
        /// Maps database entity to entity model.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{Foo}"/> instance.</param>
        public virtual void Map(EntityTypeBuilder<Foo> builder)
        {
            builder
                .Property(p => p.FluentApiString)
                .HasMaxLength(200);
        }
    }
}