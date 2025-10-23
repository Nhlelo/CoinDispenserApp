using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CoinDispenser.Core.Interfaces;
using CoinDispenser.Api.DTOs;
using CoinDispenser.Api.Data;
using CoinDispenser.Api.Models;

namespace CoinDispenser.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoinChangeController : ControllerBase
    {
        private readonly ICoinChangeService _svc;
        private readonly CoinChangeDbContext _db;

        public CoinChangeController(ICoinChangeService svc, CoinChangeDbContext db)
        {
            _svc = svc;
            _db = db;
        }

        // POST api/coinchange
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChangeRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var denoms = dto.Denominations!.Distinct().OrderBy(x => x).ToList();
            if (!denoms.Any() || denoms.First() != 1) return BadRequest("Denominations must include 1 and be non-empty.");

            var result = _svc.GetMinimumCoins(denoms, dto.Amount);

            // Persist request/response
            var entity = new ChangeEntity
            {
                Denominations = string.Join(",", denoms),
                Amount = dto.Amount,
                ResultJson = JsonSerializer.Serialize(result),
                Success = result.Success
            };
            _db.ChangeRequests.Add(entity);
            await _db.SaveChangesAsync();

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        // GET api/coinchange/history
        [HttpGet("history")]
        public IActionResult History(int page = 1, int pageSize = 20)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 20 : pageSize;
            var items = _db.ChangeRequests
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(items);
        }
    }
}