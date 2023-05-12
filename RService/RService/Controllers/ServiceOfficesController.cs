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
    public class ServiceOfficesController : ControllerBase
    {
        private readonly RServiceContext _context;

        public ServiceOfficesController(RServiceContext context)
        {
            _context = context;
        }

        // GET: api/ServiceOffices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceOffice>>> GetServiceOffice()
        {
          if (_context.ServiceOffice == null)
          {
              return NotFound();
          }
            return await _context.ServiceOffice.ToListAsync();
        }

        // GET: api/ServiceOffices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceOffice>> GetServiceOffice(int id)
        {
          if (_context.ServiceOffice == null)
          {
              return NotFound();
          }
            var serviceOffice = await _context.ServiceOffice.FindAsync(id);

            if (serviceOffice == null)
            {
                return NotFound();
            }

            return serviceOffice;
        }

        // PUT: api/ServiceOffices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceOffice(int id, ServiceOffice serviceOffice)
        {
            if (id != serviceOffice.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceOffice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceOfficeExists(id))
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

        // POST: api/ServiceOffices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceOffice>> PostServiceOffice(ServiceOffice serviceOffice)
        {
          if (_context.ServiceOffice == null)
          {
              return Problem("Entity set 'RServiceContext.ServiceOffice'  is null.");
          }
            _context.ServiceOffice.Add(serviceOffice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceOffice", new { id = serviceOffice.Id }, serviceOffice);
        }

        // DELETE: api/ServiceOffices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceOffice(int id)
        {
            if (_context.ServiceOffice == null)
            {
                return NotFound();
            }
            var serviceOffice = await _context.ServiceOffice.FindAsync(id);
            if (serviceOffice == null)
            {
                return NotFound();
            }

            _context.ServiceOffice.Remove(serviceOffice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceOfficeExists(int id)
        {
            return (_context.ServiceOffice?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
