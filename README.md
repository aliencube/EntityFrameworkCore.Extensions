# EntityFrameworkCore.Extensions #

[![Build status](https://ci.appveyor.com/api/projects/status/olv0orpfr42ct77f/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/entityframeworkcore-extensions/branch/dev) [![](https://img.shields.io/nuget/v/Aliencube.EntityFrameworkCore.Extensions.svg)](https://www.nuget.org/packages/Aliencube.EntityFrameworkCore.Extensions/)

**Entity Framework Core Extensions (EFCE)** provides extension methods to help build entities through Entity Framework Core (EF Core).


## Getting Started ##

When a database context class inheriting the `DbContext` class is written through EF Core, there are a few ways to setup data types, relations and so on. With **EFCE**, those entity building gets much easier. Here's an example.

```csharp
public class Product
{
  public Product()
  {
      this.ProductPrices = new List<ProductPrice>();
  }

  public int ProductId { get; set; }

  public string Name { get; set; }

  public List<ProductPrice> ProductPrices { get; set; }
}

public class ProductPrice
{
  public int ProductPriceId { get; set; }

  public int ProductId { get; set; }

  public decimal Value { get; set; }

  public DateTimeOffset ValidFrom { get; set; }

  public DateTimeOffset? ValidTo { get; set; }

  public Product Product { get; set; }
}

public class ProductDbContext : DbContext
{
  public ProductDbContext()
      : base()
  {
  }

  public ProductDbContext(DbContextOptions<ProductDbContext> options)
      : base(options)
  {
  }

  public DbSet<Product> Products { get; set; }

  public DbSet<ProductPrice> ProductPrices { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    ...
  }
}
```

From the official EF Core document, [Relational Database Modeling](https://docs.efproject.net/en/latest/modeling/relational/index.html), we should be following this way:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  modelBuilder.Entity<ProductPrice>()
              .HasOne(p => p.Product)
              .WithMany(b => b.ProductPrices)
              .HasForeignKey(p => p.ProductId);
}
```

However, if number of entities are increasing, it would be a good idea to separate those entity building logic from the one main method. **EFCE** exactly does the job like:

```csharp
public static class EntityTypeBuilderExtenions
{
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

public class ProductMap : IEntityMapper<Product>
{
  public void Map(EntityTypeBuilder<Product> builder)
  {
    // Primary Key
    builder.HasKey(p => p.ProductId);

    // Properties
    builder.Property(p => p.ProductId).IsRequired().UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
    builder.Property(p => p.Name).IsRequired().HasMaxLength(64);

    // Table & Column Mappings
    builder.ToTable("Product");
    builder.Property(p => p.ProductId).HasColumnName("ProductId");
    builder.Property(p => p.Name).HasColumnName("Name");
  }
}

public class ProductPriceMap : IEntityMapper<ProductPrice>
{
  public void Map(EntityTypeBuilder<ProductPrice> builder)
  {
    // Primary Key
    builder.HasKey(p => p.ProductPriceId);

    // Properties
    builder.Property(p => p.ProductPriceId).IsRequired().UseSqlServerIdentityColumn().ValueGeneratedOnAdd();
    builder.Property(p => p.ProductId).IsRequired();
    builder.Property(p => p.Value).IsRequired();
    builder.Property(p => p.ValidFrom).IsRequired();
    builder.Property(p => p.ValidTo).IsRequired(false);

    // Table & Column Mappings
    builder.ToTable("ProductPrice");
    builder.Property(p => p.ProductPriceId).HasColumnName("ProductPriceId");
    builder.Property(p => p.ProductId).HasColumnName("ProductId");
    builder.Property(p => p.Value).HasColumnName("Value");
    builder.Property(p => p.ValidFrom).HasColumnName("ValidFrom");
    builder.Property(p => p.ValidTo).HasColumnName("ValidTo");

    // Relationships
    builder.HasOne(p => p.Product)
           .WithMany(p => p.ProductPrices)
           .HasForeignKey(p => p.ProductId);
  }
}
```

Hence, the `OnModelCreating` method of the `DbContext` class can be updated like:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
  builder.Entity<Product>().Map(new ProductMap());
  builder.Entity<ProductPrice>().Map(new ProductPriceMap());
}
```

## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work, please send us a pull request onto our `dev` branch for review.


## License ##

**Entity Framework Core Extensions (EFCE)** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2016 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
