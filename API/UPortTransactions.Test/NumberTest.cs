using FluentAssertions;
using System;
using System.Globalization;
using Xunit;

namespace UPortTransactions.Test
{
    public class NumberTest
    {
        [Fact]
        public void HexToInt64_Should_Work()
        {
            // arrange
            var hexToConvert = "2ce3a2";
            Int64 number;
            var styles = NumberStyles.HexNumber;
            var provider = CultureInfo.InvariantCulture;

            // act
            Int64.TryParse(hexToConvert, styles, provider, out number);

            // assert
            number.Should().Be(2941858);
        }
    }
}
