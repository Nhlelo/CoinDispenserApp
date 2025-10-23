using System;
using System.Collections.Generic;
using System.Linq;
using CoinDispenser.Core.Interfaces;
using CoinDispenser.Core.Models;

namespace CoinDispenser.Core.Services
{
    // Implementation of the coin change algorithm using dynamic programming.
    // Time complexity: O(N * amount) where N = number of denominations.
    public class CoinChangeService : ICoinChangeService
    {
        // Computes the minimum coins needed for 'amount' with given 'denominations'.
        // Returns a ChangeResult: Success false with Error when input invalid or no solution found.
        // Important algorithm comments:
        // - dp[v] holds the minimal number of coins required to build value v.
        // - choice[v] stores the last coin used to get dp[v] (for reconstruction).
        // - Initialization: dp[0] = 0; dp[>0] = INF.
        // - For each value v from 1..amount, try every coin <= v and update dp[v] = min(dp[v], dp[v-coin] + 1).
        // - After dp computed, if dp[amount] is INF => no solution. Otherwise trace back using choice[].
        public ChangeResult GetMinimumCoins(IReadOnlyList<int> denominations, int amount)
        {
            // Validate amount
            if (amount < 0)
            {
                return new ChangeResult { Success = false, Error = "Amount cannot be negative." };
            }

            // Validate denominations: must be present, contain 1 (so every amount reachable), and positive.
            if (denominations == null || denominations.Count == 0 || denominations.Any(d => d <= 0))
            {
                return new ChangeResult { Success = false, Error = "Denominations must be non-empty positive integers." };
            }

            // Ensure at least coin 1 present so any amount is reachable; if not present we still try but will return error if impossible.
            var sorted = denominations.Distinct().OrderBy(d => d).ToArray();
            if (sorted[0] != 1)
            {
                return new ChangeResult { Success = false, Error = "Denominations must include 1." };
            }

            // dp[v] = minimum number of coins to make v
            const int INF = int.MaxValue / 4;
            var dp = new int[amount + 1];
            var choice = new int[amount + 1]; // last coin used to reach v

            // base
            dp[0] = 0;
            choice[0] = -1;

            // fill dp table
            for (int v = 1; v <= amount; v++)
            {
                dp[v] = INF;
                choice[v] = -1;

                // try each coin (sorted ascending); break early when coin > v
                foreach (var coin in sorted)
                {
                    if (coin > v) break;

                    // If dp[v - coin] is reachable, consider using coin
                    if (dp[v - coin] + 1 < dp[v])
                    {
                        dp[v] = dp[v - coin] + 1;
                        choice[v] = coin;
                    }
                }
            }

            // if amount not reachable
            if (dp[amount] >= INF || choice[amount] == -1)
            {
                return new ChangeResult { Success = false, Error = "Cannot make change for the given amount with provided denominations." };
            }

            // reconstruct counts per denomination
            var counts = sorted.ToDictionary(d => d, _ => 0);
            int remaining = amount;
            while (remaining > 0)
            {
                var used = choice[remaining];
                if (used <= 0) break; // defensive
                counts[used]++;
                remaining -= used;
            }

            return new ChangeResult { Success = true, TotalCoins = dp[amount], Coins = counts };
        }
    }
}
