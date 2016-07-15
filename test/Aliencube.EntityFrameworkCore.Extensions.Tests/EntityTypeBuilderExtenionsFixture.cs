using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests
{
    public class EntityTypeBuilderExtenionsFixture
    {
        [Fact]
        public void Map_has_guardclause1()
        {
            Assert.Throws<ArgumentNullException>(
                () => EntityTypeBuilderExtenions.Map<Foo, FooMapper>(null, null));
        }

        [Fact]
        public void Map_has_guardclause2()
        {
            var options = CreateDbContextOptions<FooContext>();
            var mapperMock = new Mock<FooMapper>();
            using (var context = new FooContext(options, null))
            {
                Assert.Throws<ArgumentNullException>(
                    () => context.Model.GetAnnotations());
            }
        }

        [Fact]
        public void Map_should_be_called()
        {
            var options = CreateDbContextOptions<FooContext>();
            var mapperMock = new Mock<FooMapper>();
            using (var context = new FooContext(options, mapperMock.Object))
            {
                context.Model.GetAnnotations();
            }

            mapperMock.Verify(m => m.Map(It.IsAny<EntityTypeBuilder<Foo>>()), Times.Once);
        }

        private static DbContextOptions CreateDbContextOptions<T>()
             where T : DbContext
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }

    public class Foo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FooMapper : IEntityMapper<Foo>
    {
        public virtual void Map(EntityTypeBuilder<Foo> builder)
        {
        }
    }

    public class FooContext : DbContext
    {
        private readonly FooMapper mapper;
        public FooContext(DbContextOptions options, FooMapper mapper)
            : base(options)
        {
            this.mapper = mapper;
        }

        public DbSet<Foo> Fooes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Foo>().Map(mapper);
        }
    }
}
