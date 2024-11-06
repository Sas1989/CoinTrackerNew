using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain;

namespace CoinList.Tests.Domain.ValueObjects
{
    public class SymbolTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_Should_ThrowArgumentException_WhenSymbolIsInvalid(string symbol)
        {
            Result<Symbol> result = Symbol.Create(symbol);

            Assert.True(result.IsFailure);
            Assert.Equal(CoinError.SymbolIsEmpty, result.Error);

        }

        [Fact]
        public void Constructor_Should_MakeValueUpperCase()
        {
            var symbol = "symbol";

            Result<Symbol> result = Symbol.Create(symbol);

            Assert.Equal(symbol.ToUpperInvariant(), result.Value?.Value);
        }
    }
}
