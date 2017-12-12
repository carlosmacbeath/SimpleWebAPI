using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/controller")]
    public class AmenitiesController : Controller
    {
        private readonly TodoContext _context;

        public AmenitiesController(TodoContext context)
        {
            _context = context;

            if (_context.AmenityItems.Count() == 0)
            {
                _context.AmenityItems.Add(new Amenity { Id = 1, AmenityName = "Unrivaled customer service", Deleted=false , ShowOnSearch = true , SortOrder = false });
                _context.AmenityItems.Add(new Amenity { Id = 2, AmenityName = "Newest, most innovative fleet", Deleted = false, ShowOnSearch = true, SortOrder = true });
                _context.AmenityItems.Add(new Amenity { Id = 3, AmenityName = "The Moorings’ 4-Hour Guarantee", Deleted = false, ShowOnSearch = true, SortOrder = false });
                _context.AmenityItems.Add(new Amenity { Id = 4, AmenityName = "Complimentary Friendly Skipper", Deleted = false, ShowOnSearch = true, SortOrder = false });
                _context.AmenityItems.Add(new Amenity { Id = 5, AmenityName = "Easy Planning & Preparation", Deleted = false, ShowOnSearch = true, SortOrder = false });
                _context.AmenityItems.Add(new Amenity { Id = 6, AmenityName = "Water, Ice & Fuel", Deleted = false, ShowOnSearch = true, SortOrder = false });

                _context.SaveChanges();
            }

        }

        // GET: api/Amenities
        [HttpGet]
        public IEnumerable<Amenity> GetAmenityItems()
        {
            return _context.AmenityItems;
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var amenity = await _context.AmenityItems.SingleOrDefaultAsync(m => m.Id == id);

            if (amenity == null)
            {
                return NotFound();
            }

            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity([FromRoute] int id, [FromBody] Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != amenity.Id)
            {
                return BadRequest();
            }
            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
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

        // POST: api/Amenities
        [HttpPost]
        public async Task<IActionResult> PostAmenity([FromBody] Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AmenityItems.Add(amenity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var amenity = await _context.AmenityItems.SingleOrDefaultAsync(m => m.Id == id);
            if (amenity == null)
            {
                return NotFound();
            }
            _context.AmenityItems.Remove(amenity);
            await _context.SaveChangesAsync();
            return Ok(amenity);
        }
        private bool AmenityExists(int id)
        {
            return _context.AmenityItems.Any(e => e.Id == id);
        }
    }
}