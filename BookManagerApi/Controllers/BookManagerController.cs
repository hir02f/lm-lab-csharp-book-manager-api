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

            if (book == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return book;
            }
        }

        // PUT: api/v1/book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateBookById(long id, Book book)
        {
            if (book == null)
            {
                return new NotFoundResult(); 
            }
            else
            {
                if (_bookManagementService.Update(id, book) == null)
                {
                    return new NotFoundResult();
                }
                else
                {
                    return NoContent();
                }
            }
        }

        // POST: api/v1/book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            if (_bookManagementService.Create(book) != null)
            {
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            else
            {
                //return new Results().ValidationProblem();
                return new NotFoundResult();
            }
        }

        // DELETE: api/v1/book/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBookById(long id)
        {
            var book = _bookManagementService.FindBookById(id);

            if (_bookManagementService.FindBookById(id) != null)
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
