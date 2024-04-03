using Endpoints.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endpoints.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(MyDBContext context) : ControllerBase
    {
        private readonly MyDBContext _db = context;

        [HttpGet]
        [Route(nameof(GetBooks))]
        public IActionResult GetBooks(int limit)
        {
            ICollection<Book> books = limit != 0 ? _db.Books.Take(limit).ToList() : _db.Books.ToArray();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet]
        [Route(nameof(GetBook))]
        public IActionResult GetBook(int id)
        {
            var book = _db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Route(nameof(CreateBook))]
        public async Task<IActionResult> CreateBook(Book book)
        {
            await _db.Books.AddAsync(book);
            _db.SaveChanges();
            return CreatedAtAction("GetBook", new { id = book.Id });
        }

        [HttpPut]
        [Route(nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            var bookToUpdate = await _db.Books.SingleOrDefaultAsync(b => b.Id == book.Id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            bookToUpdate.Description = book.Description;
            bookToUpdate.Title = book.Title;

            await _db.SaveChangesAsync();

            return Ok(bookToUpdate);
        }
        
        [HttpDelete]
        [Route(nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook(Book book)
        {
            var bookToUpdate = await _db.Books.SingleOrDefaultAsync(b => b.Id == book.Id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            _db.Books.Remove(bookToUpdate);
            await _db.SaveChangesAsync();
            return Ok("Book is successfully deleted");
        }
    }
}
