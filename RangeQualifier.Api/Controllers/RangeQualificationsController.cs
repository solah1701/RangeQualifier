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
    public class RangeQualificationsController : ControllerBase
    {
        private readonly RangeQualifierContext _context;

        public RangeQualificationsController(RangeQualifierContext context)
        {
            _context = context;
        }

        // GET: api/RangeQualifications
        [HttpGet]
        public IEnumerable<RangeQualification> GetRangeQualification()
        {
            return _context.RangeQualification;
        }

        // GET: api/RangeQualifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRangeQualification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rangeQualification = await _context.RangeQualification.FindAsync(id);

            if (rangeQualification == null)
            {
                return NotFound();
            }

            return Ok(rangeQualification);
        }

        // PUT: api/RangeQualifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRangeQualification([FromRoute] int id, [FromBody] RangeQualification rangeQualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rangeQualification.Id)
            {
                return BadRequest();
            }

            _context.Entry(rangeQualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RangeQualificationExists(id))
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

        // POST: api/RangeQualifications
        [HttpPost]
        public async Task<IActionResult> PostRangeQualification([FromBody] RangeQualification rangeQualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RangeQualification.Add(rangeQualification);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RangeQualificationExists(rangeQualification.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRangeQualification", new { id = rangeQualification.Id }, rangeQualification);
        }

        // DELETE: api/RangeQualifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRangeQualification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rangeQualification = await _context.RangeQualification.FindAsync(id);
            if (rangeQualification == null)
            {
                return NotFound();
            }

            _context.RangeQualification.Remove(rangeQualification);
            await _context.SaveChangesAsync();

            return Ok(rangeQualification);
        }

        private bool RangeQualificationExists(int id)
        {
            return _context.RangeQualification.Any(e => e.Id == id);
        }
    }
}