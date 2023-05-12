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
    public class SpecialistServicesController : ControllerBase
    {
        private readonly RServiceContext _context;

        public SpecialistServicesController(RServiceContext context)
        {
            _context = context;
        }

        // GET: api/SpecialistServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialistService>>> GetSpecialistService()
        {
          if (_context.SpecialistService == null)
          {
              return NotFound();
          }
            return await _context.SpecialistService.ToListAsync();
        }

        // GET: api/SpecialistServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialistService>> GetSpecialistService(int id)
        {
          if (_context.SpecialistService == null)
          {
              return NotFound();
          }
            var specialistService = await _context.SpecialistService.FindAsync(id);

            if (specialistService == null)
            {
                return NotFound();
            }

            return specialistService;
        }

        // PUT: api/SpecialistServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialistService(int id, SpecialistService specialistService)
        {
            if (id != specialistService.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialistService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialistServiceExists(id))
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

        // POST: api/SpecialistServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecialistService>> PostSpecialistService(SpecialistService specialistService)
        {
          if (_context.SpecialistService == null)
          {
              return Problem("Entity set 'RServiceContext.SpecialistService'  is null.");
          }
            _context.SpecialistService.Add(specialistService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialistService", new { id = specialistService.Id }, specialistService);
        }

        // DELETE: api/SpecialistServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialistService(int id)
        {
            if (_context.SpecialistService == null)
            {
                return NotFound();
            }
            var specialistService = await _context.SpecialistService.FindAsync(id);
            if (specialistService == null)
            {
                return NotFound();
            }

            _context.SpecialistService.Remove(specialistService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialistServiceExists(int id)
        {
            return (_context.SpecialistService?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
