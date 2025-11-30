using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("{routeId}")]
        public async Task<ActionResult<Author>> GetAuthor(string routeId)
        {
            if (!int.TryParse(routeId, out int id)) return NotFound();

            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            return author;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDto dto)
        {
            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthor), new { routeId = author.Id.ToString() }, author);
        }

        [HttpPut("{routeId}")]
        public async Task<IActionResult> PutAuthor(string routeId, [FromBody] AuthorDto dto)
        {
            if (!int.TryParse(routeId, out int id)) return NotFound();

            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{routeId}")]
        public async Task<IActionResult> DeleteAuthor(string routeId)
        {
            if (!int.TryParse(routeId, out int id)) return NotFound();

            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}