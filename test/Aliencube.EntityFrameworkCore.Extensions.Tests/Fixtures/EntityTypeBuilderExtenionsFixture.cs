using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures
{
    public class Foo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string AlreadyMappedByOthers { get; set; }

        [MaxLength(200)]
        public string AnnotatedString { get; set; }

        public string FluentApiString { get; set; }
    }

    public class FooMapper : IEntityMapper<Foo>
    {
        public virtual void Map(EntityTypeBuilder<Foo> builder)
        {
            builder
                .Property(p => p.FluentApiString)
                .HasMaxLength(200);
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
            modelBuilder.Entity<Foo>().Property(m => m.AlreadyMappedByOthers).HasMaxLength(200);
            modelBuilder.Entity<Foo>().SetDefaultStringMaxLength(10);
            modelBuilder.Entity<Foo>().Map(mapper);
        }
    }
}