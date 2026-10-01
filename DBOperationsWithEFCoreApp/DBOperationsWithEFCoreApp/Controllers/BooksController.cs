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

        //Basic update method
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        //{
        //    var existingBook = await appDbContext.Books.FindAsync(id);
        //    if (existingBook == null)
        //    {
        //        return NotFound();
        //    }

        //    existingBook.Title = book.Title;
        //    existingBook.Description = book.Description;

        //    await appDbContext.SaveChangesAsync();
        //    return Ok(existingBook);
        //}

        [HttpPut("")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            appDbContext.Entry(book).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBookInBulk()
        {
            await appDbContext.Books
                .Where(book => book.Id > 6)
                .ExecuteUpdateAsync(b => b.SetProperty(book => book.IsActive, book => false)
                .SetProperty(book => book.Title, book => book.Title + " Updated"));

            return Ok();            
        }
    }
}
