using DBOperationsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DBOperationsWithEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] Book book)
        {
            appDbContext.Books.Add(book);
            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddNewBook([FromBody] List<Book> books)
        {
            appDbContext.Books.AddRange(books);
            await appDbContext.SaveChangesAsync();
            return Ok(books);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await appDbContext.Books.ToListAsync();
            return Ok(books);
        }
    }
}
