using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ItemsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            if (!int.TryParse(id, out int itemId)) return NotFound();

            var item = await _context.Items.FindAsync(itemId);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            if (!await _context.Books.AnyAsync(b => b.Id == item.BookId)) return BadRequest("Book does not exist");
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id.ToString() }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string id, Item item)
        {
            if (!int.TryParse(id, out int itemId)) return NotFound();

            if (item.Id != 0 && item.Id != itemId) return BadRequest();
            item.Id = itemId;

            if (!await _context.Books.AnyAsync(b => b.Id == item.BookId)) return BadRequest("Book does not exist");

            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Items.Any(e => e.Id == itemId)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            if (!int.TryParse(id, out int itemId)) return NotFound();

            var item = await _context.Items.FindAsync(itemId);
            if (item == null) return NotFound();
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}