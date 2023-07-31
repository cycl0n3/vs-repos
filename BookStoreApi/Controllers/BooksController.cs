using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        // GET: /Books
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get() =>
            await _booksService.GetAsync();

        // GET: /Books/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: /Books
        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            await _booksService.CreateAsync(book);

            return CreatedAtRoute(nameof(Get), new { id = book.Id.ToString() }, book);
        }

        // PUT: /Books/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book bookIn)
        {
            var book = await _booksService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _booksService.UpdateAsync(id, bookIn);

            return NoContent();
        }

        // DELETE: /Books/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _booksService.DeleteAsync(book.Id);

            return NoContent();
        }
    }
}
