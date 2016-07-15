using System;

using Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using Xunit;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="EntityTypeBuilderExtenions"/> class.
    /// </summary>
    public class EntityTypeBuilderExtenionsTest : IClassFixture<EntityTypeBuilderExtenionsFixture>
    {
        private readonly EntityTypeBuilder<Foo> _builder;
        private readonly Mock<FooMapper> _mapper;

        public EntityTypeBuilderExtenionsTest(EntityTypeBuilderExtenionsFixture fixture)
        {
            this._builder = fixture.EntityTypeBuilder;
            this._mapper = fixture.FooMapper;
        }

        [Fact]
        public void Given_NullParameter_Map_ShouldThrow_Exception()
        {
            Action action = () => { var result = EntityTypeBuilderExtenions.Map<Foo, FooMapper>(null, this._mapper.Object); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var result = EntityTypeBuilderExtenions.Map<Foo, FooMapper>(this._builder, null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            var options = CreateDbContextOptions<FooContext>();

            Action action = () => { var context = new FooContext(options, null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Given_That_Map_ShouldBe_Called()
        {
            var options = CreateDbContextOptions<FooContext>();
            using (var context = new FooContext(options, this._mapper.Object))
            {
                context.Model.GetAnnotations();
            }

            this._mapper.Verify(m => m.Map(It.IsAny<EntityTypeBuilder<Foo>>()), Times.Once);
        }

        [Fact]
        public void Given_That_SetDefaultStringMaxLength_ShouldBe_Set()
        {
            IEntityType entityType;
            var options = CreateDbContextOptions<FooContext>();
            using (var context = new FooContext(options, new FooMapper()))
            {
                context.Model.GetAnnotations();
                entityType = context.Model.FindEntityType(typeof(Foo));
            }

            var fooName = entityType.FindProperty(nameof(Foo.Name));
            var tagName = entityType.FindProperty(nameof(Foo.Tag));

            fooName.GetMaxLength().GetValueOrDefault().Should().Be(10);
            tagName.GetMaxLength().GetValueOrDefault().Should().Be(10);
        }

        [Fact]
        public void Given_That_HasMaxLength_ShouldOverride_DefaultMaxLength()
        {
            IEntityType entityType;
            var options = CreateDbContextOptions<FooContext>();
            using (var context = new FooContext(options, new FooMapper()))
            {
                context.Model.GetAnnotations();
                entityType = context.Model.FindEntityType(typeof(Foo));
            }

            var fooAlreadyMappedByOthers = entityType.FindProperty(nameof(Foo.AlreadyMappedByOthers));
            var fooAnnotatedString = entityType.FindProperty(nameof(Foo.AnnotatedString));
            var fooFluentApiString = entityType.FindProperty(nameof(Foo.FluentApiString));

            fooAlreadyMappedByOthers.GetMaxLength().GetValueOrDefault().Should().Be(200);
            fooAnnotatedString.GetMaxLength().GetValueOrDefault().Should().Be(200);
            fooFluentApiString.GetMaxLength().GetValueOrDefault().Should().Be(200);
        }

        private static DbContextOptions CreateDbContextOptions<T>() where T : DbContext
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