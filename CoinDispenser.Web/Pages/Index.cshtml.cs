
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using CoinDispenser.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CoinDispenser.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpFactory;

        public IndexModel(IConfiguration config, IHttpClientFactory httpFactory)
        {
            _config = config;
            _httpFactory = httpFactory;
        }

        [BindProperty]
        [Required]
        public string DenominationsInput { get; set; } = "1,2,5,10";

        [BindProperty]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; } = 0;

        public ChangeResult? Result { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Parse denominations
            List<int> denoms;
            try
            {
                denoms = DenominationsInput
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s.Trim()))
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();
            }
            catch
            {
                ModelState.AddModelError(nameof(DenominationsInput), "Invalid denominations format.");
                return Page();
            }

            var dto = new { Denominations = denoms, Amount = Amount };
            var client = _httpFactory.CreateClient();
            var apiBase = _config["ApiBaseUrl"]?.TrimEnd('/') ?? "https://localhost:44373/api";
            var requestUri = $"{apiBase}/coinchange";
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add API key header (same key as configured in API)
            //client.DefaultRequestHeaders.Add("X-Api-Key", _config["ApiKey"] ?? "super-secret-api-key");

            try
            {
                var resp = await client.PostAsync(requestUri, content);
                var respContent = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                {
                    ErrorMessage = $"API error: {respContent}";
                    return Page();
                }

                Result = JsonSerializer.Deserialize<ChangeResult>(respContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Could not contact API: " + ex.Message;
                return Page();
            }
        }
    }
}