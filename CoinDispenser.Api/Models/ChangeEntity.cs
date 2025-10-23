using System.ComponentModel.DataAnnotations;

namespace CoinDispenser.Api.Models
{
    public class ChangeEntity
    {
        [Key]
        public int Id { get; set; }
        public string Denominations { get; set; } = "";
        public int Amount { get; set; }
        public string ResultJson { get; set; } = "";
        public bool Success { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
