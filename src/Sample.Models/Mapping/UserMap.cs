using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Models.Mapping
{
    /// <summary>
    /// This represents the mapper entity for the <see cref="User"/> class.
    /// </summary>
    public class UserMap : IEntityMapper<User>
    {
        /// <summary>
        /// Maps database entity to entity model.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{User}"/> instance.</param>
        public void Map(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(p => p.UserId);

            // Properties
            builder.Property(p => p.UserId).IsRequired().UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(64);

            // Table & Column Mappings
            builder.ToTable("User");
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.Property(p => p.Name).HasColumnName("Name");
        }
    }
}