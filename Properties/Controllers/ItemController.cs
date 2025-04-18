using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToCarAPI.Data;
using ToCarAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ToCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.Include(i => i.Category).ToListAsync();
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // GET: api/Item/search?term=keyword
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Item>>> SearchItems(string term , int? categoryId)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return await GetItems();
            }

            term = term.ToLower();
            var query = _context.Items
                .Where(i => i.Title.ToLower().Contains(term) || 
                       i.PartCode.ToLower().Contains(term)
                       )
                .Include(i => i.Category);
                if(categoryId != null)
                {
                    query.Where(x=>x.CategoryId == categoryId);
                }
            return await query.ToListAsync();
        }

        // GET: api/Item/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByCategory(int categoryId)
        {
            return await _context.Items
                .Where(i => i.CategoryId == categoryId)
                .ToListAsync();
        }

        // POST: api/Item
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}