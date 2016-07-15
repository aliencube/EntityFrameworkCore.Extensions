using System;

using Microsoft.EntityFrameworkCore;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures
{
    /// <summary>
    /// This represents the DB context entity.
    /// </summary>
    public class FooContext : DbContext
    {
        private readonly FooMapper mapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="FooContext"/> class.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/> instance.</param>
        /// <param name="mapper"><see cref="FooMapper"/> instance.</param>
        public FooContext(DbContextOptions options, FooMapper mapper) : base(options)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.mapper = mapper;
        }

        /// <summary>
        /// Gets or sets the set of <see cref="Foo"/> objects.
        /// </summary>
        public DbSet<Foo> Fooes { get; set; }

        /// <summary>
        /// Called while entity models are created.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/> instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Foo>().Property(m => m.AlreadyMappedByOthers).HasMaxLength(200);
            modelBuilder.Entity<Foo>().SetDefaultStringMaxLength(10);
            modelBuilder.Entity<Foo>().Map(mapper);
        }
    }
}