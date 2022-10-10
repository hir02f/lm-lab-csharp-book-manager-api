using Microsoft.AspNetCore.Mvc;
using BookManagerApi.Models;
using BookManagerApi.Services;

namespace BookManagerApi.Controllers
{
    [Route("api/v1/book")]
    [ApiController]
    public class BookManagerController : ControllerBase
    {
        private readonly IBookManagementService _bookManagementService;

        public BookManagerController(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        // GET: api/v1/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return _bookManagementService.GetAllBooks();
        }

        // GET: api/v1/book/5
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            var book = _bookManagementService.FindBookById(id);
            return book;
        }

        // PUT: api/v1/book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateBookById(long id, Book book)
        {
            if (book == null)
            {
                return NotFound(); // status code 404 returned , not as good as 204
            }
            else
            {
                _bookManagementService.Update(id, book);
                return NoContent();
            }
        }

        // POST: api/v1/book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            _bookManagementService.Create(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // DELETE: api/v1/book/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBookById(long id)
        {
            var book = _bookManagementService.FindBookById(id);

            if (book != null)
            {
                _bookManagementService.Delete(book);
                return NoContent();
            }
            else
            {
                return NotFound(); // status code 404 returned , not as good as 204
            }
        }
    }
}
