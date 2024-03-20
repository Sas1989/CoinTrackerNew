using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinList.Infrastrcture;

public class CoinListMapper : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Symbol).IsRequired().HasConversion(x => x.Value, x => Symbol.Create(x).Value);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50).HasConversion(x => x.Value, x => Name.Create(x).Value);
        builder.Property(x => x.Price).IsRequired().HasConversion(x => x.Value, x => Price.Create(x).Value).HasPrecision(18,8);

        builder.ToTable(x => x.HasCheckConstraint("CK_CoinList_PriceNegative", "Price >= 0"));
        builder.HasIndex(x => x.Symbol).IsUnique();
    }
}
