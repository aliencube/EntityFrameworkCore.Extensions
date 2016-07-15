using System;

using Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using Xunit;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests
{
    public class EntityTypeBuilderExtenionsTest
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

        [Fact]
        public void SetDefaultStringMaxLength_configures_MaxLength()
        {
            IEntityType entityType = null;
            var options = CreateDbContextOptions<FooContext>();
            using (var context = new FooContext(options, new FooMapper()))
            {
                context.Model.GetAnnotations();
                entityType = context.Model.FindEntityType(typeof(Foo));
            }

            var fooName = entityType.FindProperty(nameof(Foo.Name));
            var tagName = entityType.FindProperty(nameof(Foo.Tag));
            Assert.Equal(10, fooName.GetMaxLength().Value);
            Assert.Equal(10, tagName.GetMaxLength().Value);
        }

        [Fact]
        public void SetDefaultStringMaxLength_is_not_affected_for_explicit_MaxLength_configurations()
        {
            IEntityType entityType = null;
            var options = CreateDbContextOptions<FooContext>();
            using (var context = new FooContext(options, new FooMapper()))
            {
                context.Model.GetAnnotations();
                entityType = context.Model.FindEntityType(typeof(Foo));
            }

            var fooAlreadyMappedByOthers = entityType.FindProperty(nameof(Foo.AlreadyMappedByOthers));
            var fooAnnotatedString = entityType.FindProperty(nameof(Foo.AnnotatedString));
            var fooFluentApiString = entityType.FindProperty(nameof(Foo.FluentApiString));
            Assert.Equal(200, fooAlreadyMappedByOthers.GetMaxLength().Value);
            Assert.Equal(200, fooAnnotatedString.GetMaxLength().Value);
            Assert.Equal(200, fooFluentApiString.GetMaxLength().Value);
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
}