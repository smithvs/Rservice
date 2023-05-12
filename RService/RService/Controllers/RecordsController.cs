﻿using System;
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
    public class RecordsController : ControllerBase
    {
        private readonly RServiceContext _context;

        public RecordsController(RServiceContext context)
        {
            _context = context;
        }

        // GET: api/Records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecord()
        {
          if (_context.Record == null)
          {
              return NotFound();
          }
            return await _context.Record.ToListAsync();
        }

        // GET: api/Records/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(int id)
        {
          if (_context.Record == null)
          {
              return NotFound();
          }
            var @record = await _context.Record.FindAsync(id);

            if (@record == null)
            {
                return NotFound();
            }

            return @record;
        }

        // PUT: api/Records/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(int id, Record @record)
        {
            if (id != @record.Id)
            {
                return BadRequest();
            }

            _context.Entry(@record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
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

        // POST: api/Records
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record @record)
        {
          if (_context.Record == null)
          {
              return Problem("Entity set 'RServiceContext.Record'  is null.");
          }
            _context.Record.Add(@record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecord", new { id = @record.Id }, @record);
        }

        // DELETE: api/Records/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            if (_context.Record == null)
            {
                return NotFound();
            }
            var @record = await _context.Record.FindAsync(id);
            if (@record == null)
            {
                return NotFound();
            }

            _context.Record.Remove(@record);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordExists(int id)
        {
            return (_context.Record?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
