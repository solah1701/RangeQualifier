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
    public class BowTypesController : ControllerBase
    {
        private readonly RangeQualifierContext _context;

        public BowTypesController(RangeQualifierContext context)
        {
            _context = context;
        }

        // GET: api/BowTypes
        [HttpGet]
        public IEnumerable<BowType> GetBowType()
        {
            return _context.BowType;
        }

        // GET: api/BowTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBowType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bowType = await _context.BowType.FindAsync(id);

            if (bowType == null)
            {
                return NotFound();
            }

            return Ok(bowType);
        }

        // PUT: api/BowTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBowType([FromRoute] int id, [FromBody] BowType bowType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bowType.Id)
            {
                return BadRequest();
            }

            _context.Entry(bowType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BowTypeExists(id))
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

        // POST: api/BowTypes
        [HttpPost]
        public async Task<IActionResult> PostBowType([FromBody] BowType bowType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BowType.Add(bowType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BowTypeExists(bowType.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBowType", new { id = bowType.Id }, bowType);
        }

        // DELETE: api/BowTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBowType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bowType = await _context.BowType.FindAsync(id);
            if (bowType == null)
            {
                return NotFound();
            }

            _context.BowType.Remove(bowType);
            await _context.SaveChangesAsync();

            return Ok(bowType);
        }

        private bool BowTypeExists(int id)
        {
            return _context.BowType.Any(e => e.Id == id);
        }
    }
}