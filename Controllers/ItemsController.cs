﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postgresql.CRUD.Models;

namespace Postgresql.CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MydatabaseContext _context;

        public ItemsController(MydatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(long id, Item item)
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
          if (_context.Items == null)
          {
              return Problem("Entity set 'MydatabaseContext.Items'  is null.");
          }
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(long id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
