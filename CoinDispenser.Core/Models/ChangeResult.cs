using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinDispenser.Core.Models
{
    public class ChangeResult
    {
        public bool Success { get; init; }
        public int TotalCoins { get; init; }
        public IReadOnlyDictionary<int, int> Coins { get; init; } = new Dictionary<int, int>();
        public string? Error { get; init; }

    }
}
