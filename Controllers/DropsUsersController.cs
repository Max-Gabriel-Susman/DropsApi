using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DropsApi.Models;

namespace DropsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropsUsersController : ControllerBase
    {
        private readonly DropsContext _context;

        public DropsUsersController(DropsContext context)
        {
            _context = context;
        }

        // GET: api/DropsUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropsUser>>> GetDropsUsers()
        {
            return await _context.DropsUsers.ToListAsync();
        }

        // GET: api/DropsUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DropsUser>> GetDropsUser(string id)
        {
            var dropsUser = await _context.DropsUsers.FindAsync(id);

            if (dropsUser == null)
            {
                return NotFound();
            }

            return dropsUser;
        }

        // PUT: api/DropsUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDropsUser(string id, DropsUser dropsUser)
        {
            if (id != dropsUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(dropsUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DropsUserExists(id))
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

        // POST: api/DropsUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/TodoItems
        //[HttpPost]
        //public async Task<ActionResult<TodoItem>> PostDropsUser(TodoItem todoItem)
        //{
        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        //    return CreatedAtAction(nameof(GetDropsUser), new { id = todoItem.Id }, todoItem);
        //}

        [HttpPost]
        public async Task<ActionResult<DropsUser>> PostDropsUser(DropsUser dropsUser)
        {
            _context.DropsUsers.Add(dropsUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DropsUserExists(dropsUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // return CreatedAtAction("GetDropsUser", new { id = dropsUser.Id }, dropsUser);
            return CreatedAtAction(nameof(GetDropsUser), new { id = dropsUser.Id }, dropsUser);
        }

        // DELETE: api/DropsUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDropsUser(string id)
        {
            var dropsUser = await _context.DropsUsers.FindAsync(id);
            if (dropsUser == null)
            {
                return NotFound();
            }

            _context.DropsUsers.Remove(dropsUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DropsUserExists(string id)
        {
            return _context.DropsUsers.Any(e => e.Id == id);
        }
    }
}
