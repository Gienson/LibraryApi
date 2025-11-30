using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] string? authorId)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrEmpty(authorId))
            {
                if (int.TryParse(authorId, out int parsedId))
                {
                    query = query.Where(b => b.AuthorId == parsedId);
                }
                else
                {
                    return Ok(new List<Book>());
                }
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            if (!int.TryParse(id, out int bookId)) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto dto)
        {
            if (!await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId)) return BadRequest("Author does not exist");

            var book = new Book { Title = dto.Title, Year = dto.Year, AuthorId = dto.AuthorId };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            await _context.Entry(book).Reference(b => b.Author).LoadAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(string id, BookCreateDto dto)
        {
            if (!int.TryParse(id, out int bookId)) return NotFound();

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            if (!await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId)) return BadRequest("Author does not exist");

            book.Title = dto.Title;
            book.Year = dto.Year;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            if (!int.TryParse(id, out int bookId)) return NotFound();

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}