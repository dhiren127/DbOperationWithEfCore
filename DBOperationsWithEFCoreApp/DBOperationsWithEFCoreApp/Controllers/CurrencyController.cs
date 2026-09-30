using DBOperationsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOperationsWithEFCoreApp.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {            
            //var result = await _appDbContext.Currencies.ToListAsync(); // Placeholder for actual database call

            //var resultTest = await (from Currency in _appDbContext.Currencies select Currency).ToListAsync();

            var resultTest2 = await (from Currency in _appDbContext.Currencies select new Currency 
                                {  
                                    Id = Currency.Id, 
                                    Title = Currency.Title 
                                }).ToListAsync();
            return Ok(resultTest2); 
        }

        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetCurrencyByName([FromRoute]  string name)
        //{
        //    var currency = await _appDbContext.Currencies.FirstOrDefaultAsync(c => 
        //                    EF.Functions.Collate(c.Title, "SQL_Latin1_General_CP1_CI_AS") == name);
        //    if (currency == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(currency);
        //}

        //set with two parameter second is optional
        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetCurrencyByName([FromRoute] string name, [FromQuery] string? description)
        //{
        //    var currency = await _appDbContext.Currencies.FirstOrDefaultAsync(c =>
        //                    c.Title == name && (string.IsNullOrEmpty(description) || c.Description == description));
        //    if (currency == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(currency);
        //}

        //[HttpGet("{name}/{description}")]
        //public async Task<IActionResult> GetCurrencyByName([FromRoute] string name, [FromRoute] string description)
        //{
        //    var currency = await _appDbContext.Currencies.FirstOrDefaultAsync(c =>
        //                    c.Title == name && c.Description == description);
        //    if (currency == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(currency);
        //}

        //Using where condition
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name)
        {
            var currency = await _appDbContext.Currencies.Where(x => 
                x.Title == name
                ).ToListAsync();

            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromBody] List<int> ids)
        {
            var currency = await _appDbContext.Currencies.Where(x => ids.Contains(x.Id))
                .Select(x => new Currency{ 
                    Id = x.Id,
                    Title = x.Title                
                })
                .ToListAsync();

            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
    }
}
