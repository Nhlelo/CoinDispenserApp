using System.Collections.Generic;
using CoinDispenser.Core.Services;
using Xunit;

namespace CoinDispenser.Tests
{
    public class CoinChangeServiceTests
    {
        [Fact]
        public void ClassicalInput_ReturnsExpected()
        {
            var svc = new CoinChangeService();
            var denoms = new List<int> { 1, 2, 5, 10 };
            var res = svc.GetMinimumCoins(denoms, 18);

            Assert.True(res.Success);
            Assert.Equal(4, res.TotalCoins); // 10 + 5 + 2 + 1
            Assert.Equal(1, res.Coins[10]);
            Assert.Equal(1, res.Coins[5]);
            Assert.Equal(1, res.Coins[2]);
            Assert.Equal(1, res.Coins[1]);
        }

        [Fact]
        public void NegativeAmount_ReturnsError()
        {
            var svc = new CoinChangeService();
            var res = svc.GetMinimumCoins(new List<int> { 1, 2, 5 }, -5);
            Assert.False(res.Success);
        }

        [Fact]
        public void MissingOne_ReturnsError()
        {
            var svc = new CoinChangeService();
            var res = svc.GetMinimumCoins(new List<int> { 2, 5 }, 7);
            Assert.False(res.Success);
        }
    }
}