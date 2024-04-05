using Endpoints.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Endpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksCollectionController(MyDBContext context) : ControllerBase
    {
        private readonly MyDBContext _db = context;

        [HttpGet]
        [Route(nameof(GetBooksCollection))]
        public async Task<IActionResult> GetBooksCollection(int id)
        {
            var book = await _db.BooksCollections.Include(m => m.Books).FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet]
        [Route(nameof(GetBooksCollectionList))]
        public IActionResult GetBooksCollectionList(int limit)
        {
            var booksCollectionList = _db.BooksCollections.Include(m => m.Books).ToList();

            if (booksCollectionList.Count == 0)
            {
                return NotFound();
            }
            return Ok(booksCollectionList);
        }
        [HttpPost]
        [Route(nameof(CreateBooksCollection))]
        public async Task<IActionResult> CreateBooksCollection(BookCollectionCreate collection)
        {
            await _db.BooksCollections.AddAsync(new BookCollection() { Title = collection.Title });
            int id = await _db.SaveChangesAsync();

            return CreatedAtAction("GetBooksCollection", new { id });
        }



        [HttpPut]
        [Route(nameof(UpdateBooksCollection))]
        public async Task<IActionResult> UpdateBooksCollection(BookCollection bookCollection)
        {
            var booksCollectionToUpdate = await _db.BooksCollections.SingleOrDefaultAsync(b => b.Id == bookCollection.Id);
            if (booksCollectionToUpdate == null)
            {
                return NotFound();
            }
            booksCollectionToUpdate.Title = bookCollection.Title;
            var booksList = new List<Book>();
            foreach (var book in bookCollection.Books)
            {
                var currentBook = await _db.Books.SingleOrDefaultAsync(b => b.Id == book.Id);
                if (currentBook == null)
                    return NotFound();
                booksList.Add(currentBook);
            }

            booksCollectionToUpdate.Books = booksList;
            await _db.SaveChangesAsync();

            return Ok(booksCollectionToUpdate);
        }

        [HttpDelete]
        [Route(nameof(DeleteBooksCollection))]
        public async Task<IActionResult> DeleteBooksCollection(int id)
        {
            var booksCollection = await _db.BooksCollections.SingleOrDefaultAsync(book => book.Id == id);
            if (booksCollection == null)
                return NotFound();
            _db.BooksCollections.Remove(booksCollection);
            await _db.SaveChangesAsync();
            return Ok("Book Collection is successfully removed");
        }
    }
}
