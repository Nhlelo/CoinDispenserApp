using System.ComponentModel.DataAnnotations;

namespace CoinDispenser.Api.DTOs
{
    // Request DTO validated by model binding
    public class ChangeRequestDto
    {
        [Required]
        [MinLength(1)]
        public List<int>? Denominations { get; set; }

        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }

}
