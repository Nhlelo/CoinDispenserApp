using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinDispenser.Core.Models;

namespace CoinDispenser.Core.Interfaces
{
    // Defines the contract for the coin change method
    public interface ICoinChangeService
    {
        ChangeResult GetMinimumCoins(IReadOnlyList<int> denominations, int amount);
    }

}
