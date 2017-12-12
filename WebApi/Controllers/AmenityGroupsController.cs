using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/AmenityGroups")]
    public class AmenityGroupsController : Controller
    {
        private readonly TodoContext _context;

        public AmenityGroupsController(TodoContext context)
        {
            _context = context;

            if (_context.AmenityGroupItems.Count() == 0)
            {
                _context.AmenityGroupItems.Add(new AmenityGroup { Id = 1, GroupName = "Group 1", SortOrder = true, Deleted = false });
                _context.AmenityGroupItems.Add(new AmenityGroup { Id = 2, GroupName = "Group 2", SortOrder = true, Deleted = false });
                _context.AmenityGroupItems.Add(new AmenityGroup { Id = 3, GroupName = "Group 3", SortOrder = true, Deleted = false });
                _context.AmenityGroupItems.Add(new AmenityGroup { Id = 4, GroupName = "Group 4", SortOrder = true, Deleted = false });

                _context.SaveChanges();
            }
        }

        // GET: api/AmenityGroups
        [HttpGet]
        public IEnumerable<AmenityGroup> GetAmenityGroupItems()
        {
            return _context.AmenityGroupItems;
        }

        // GET: api/AmenityGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var amenityGroup = await _context.AmenityGroupItems.SingleOrDefaultAsync(m => m.Id == id);

            if (amenityGroup == null)
            {
                return NotFound();
            }

            return Ok(amenityGroup);
        }

        // PUT: api/AmenityGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenityGroup([FromRoute] int id, [FromBody] AmenityGroup amenityGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != amenityGroup.Id)
            {
                return BadRequest();
            }
            _context.Entry(amenityGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityGroupExists(id))
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

        // POST: api/AmenityGroups
        [HttpPost]
        public async Task<IActionResult> PostAmenityGroup([FromBody] AmenityGroup amenityGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AmenityGroupItems.Add(amenityGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenityGroup", new { id = amenityGroup.Id }, amenityGroup);
        }

        // DELETE: api/AmenityGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenityGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var amenityGroup = await _context.AmenityGroupItems.SingleOrDefaultAsync(m => m.Id == id);
            if (amenityGroup == null)
            {
                return NotFound();
            }

            _context.AmenityGroupItems.Remove(amenityGroup);
            await _context.SaveChangesAsync();

            return Ok(amenityGroup);
        }

        private bool AmenityGroupExists(int id)
        {
            return _context.AmenityGroupItems.Any(e => e.Id == id);
        }
    }
}