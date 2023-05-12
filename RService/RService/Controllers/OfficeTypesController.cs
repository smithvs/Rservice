using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RService.Data;
using RService.Models;

namespace RService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeTypesController : ControllerBase
    {
        private readonly RServiceContext _context;

        public OfficeTypesController(RServiceContext context)
        {
            _context = context;
        }

        // GET: api/OfficeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfficeType>>> GetOfficeType()
        {
          if (_context.OfficeType == null)
          {
              return NotFound();
          }
            return await _context.OfficeType.ToListAsync();
        }

        // GET: api/OfficeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeType>> GetOfficeType(int id)
        {
          if (_context.OfficeType == null)
          {
              return NotFound();
          }
            var officeType = await _context.OfficeType.FindAsync(id);

            if (officeType == null)
            {
                return NotFound();
            }

            return officeType;
        }

        // PUT: api/OfficeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficeType(int id, OfficeType officeType)
        {
            if (id != officeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(officeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeTypeExists(id))
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

        // POST: api/OfficeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OfficeType>> PostOfficeType(OfficeType officeType)
        {
          if (_context.OfficeType == null)
          {
              return Problem("Entity set 'RServiceContext.OfficeType'  is null.");
          }
            _context.OfficeType.Add(officeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfficeType", new { id = officeType.Id }, officeType);
        }

        // DELETE: api/OfficeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficeType(int id)
        {
            if (_context.OfficeType == null)
            {
                return NotFound();
            }
            var officeType = await _context.OfficeType.FindAsync(id);
            if (officeType == null)
            {
                return NotFound();
            }

            _context.OfficeType.Remove(officeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfficeTypeExists(int id)
        {
            return (_context.OfficeType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
