using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures
{
    public class EntityTypeBuilderExtenionsFixture : IDisposable
    {
        private bool _disposed;

        public EntityTypeBuilderExtenionsFixture()
        {
            var model = new Model();
            var metadata = new EntityType(typeof(Foo), model, ConfigurationSource.Convention);
            var modelBuilder = new InternalModelBuilder(model);
            var builder = new InternalEntityTypeBuilder(metadata, modelBuilder);

            this.EntityTypeBuilder = new EntityTypeBuilder<Foo>(builder);

            this.FooMapper = new Mock<FooMapper>();
        }

        public EntityTypeBuilder<Foo> EntityTypeBuilder { get; }

        /// <summary>
        /// Gets the <see cref="Mock{FooMapper}"/> instance.
        /// </summary>
        public Mock<FooMapper> FooMapper { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

    }
}