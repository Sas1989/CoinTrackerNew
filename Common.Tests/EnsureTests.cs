using Common.Domain;
using System;

namespace Common.Tests
{
    public class EnsureTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void NotNullOrEmpty_Should_ThrowArgumentException_WhenValueIsInvalid(string value)
        {
            Result result = Ensure.NotNullOrEmpty(value);

            Assert.True(result.IsFailure);
            Assert.Equal("value is null or empty", result.Error.Message);
        }

        [Fact]
        public void NotNullOrEmpty_Should_ReturnTheValue_WhenIsValid()
        {
            string value = "test";

            Result result = Ensure.NotNullOrEmpty(value);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NotNegative_Should_ThrowArgumentException_WhenNumberIsNegative()
        {
            Random random = new();
            decimal randomNumber = (decimal)(random.NextDouble() * -1.0);

            var result = Ensure.NotNegative(randomNumber);

            Assert.True(result.IsFailure);
            Assert.Equal("randomNumber is negative", result.Error.Message);
        }

        [Fact]
        public void NotNegative_Should_ReturnTheValue_WhenIsValid()
        {
            Random random = new();
            decimal randomNumber = (decimal)random.NextDouble();

            var result = Ensure.NotNegative(randomNumber);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void NotNegative_Should_ReturnTheValue_WhenIsZero()
        {
            decimal zero = 0;

            var result = Ensure.NotNegative(zero);

            Assert.True(result.IsSuccess);
        }
    }
}