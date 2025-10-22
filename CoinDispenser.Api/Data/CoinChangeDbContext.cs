using CoinDispenser.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinDispenser.Api.Data
{
    // EF Core DbContext that stores request history and results
    public class CoinChangeDbContext : DbContext
    {
        public CoinChangeDbContext(DbContextOptions<CoinChangeDbContext> options) : base(options) { }
        public DbSet<ChangeEntity> ChangeRequests { get; set; } = null!;
    }

}
