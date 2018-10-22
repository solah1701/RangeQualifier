using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangeQualifier.Api.Models.Db;

namespace RangeQualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RangesController : ControllerBase
    {
        private readonly RangeQualifierContext _context;

        public RangesController(RangeQualifierContext context)
        {
            _context = context;
        }

        // GET: api/Ranges
        [HttpGet]
        public IEnumerable<Range> GetRange()
        {
            return _context.Range;
        }

        // GET: api/Ranges/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRange([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var range = await _context.Range.FindAsync(id);

            if (range == null)
            {
                return NotFound();
            }

            return Ok(range);
        }

        // PUT: api/Ranges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRange([FromRoute] int id, [FromBody] Range range)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != range.Id)
            {
                return BadRequest();
            }

            _context.Entry(range).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RangeExists(id))
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

        // POST: api/Ranges
        [HttpPost]
        public async Task<IActionResult> PostRange([FromBody] Range range)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Range.Add(range);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RangeExists(range.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRange", new { id = range.Id }, range);
        }

        // DELETE: api/Ranges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRange([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var range = await _context.Range.FindAsync(id);
            if (range == null)
            {
                return NotFound();
            }

            _context.Range.Remove(range);
            await _context.SaveChangesAsync();

            return Ok(range);
        }

        private bool RangeExists(int id)
        {
            return _context.Range.Any(e => e.Id == id);
        }
    }
}