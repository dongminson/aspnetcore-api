using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore_api.Classes;
using aspnetcore_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aspnetcore_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {

        private readonly WordContext _context;

        public WordsController(WordContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWords([FromQuery] QueryParameters queryParameters)
        {
            IQueryable<WordEntry> words = _context.Words.Include(w => w.Definitions);
            if(!string.IsNullOrEmpty(queryParameters.Word))
            {
                words = words.Where(w => w.Word.ToLower().Contains(queryParameters.Word.ToLower()));
            }
            words = words.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
            if(!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(WordEntry).GetProperty(queryParameters.SortBy) != null)
                {
                    words = words.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }
            return Ok(await words.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWord(int id)
        {
            var word = await _context.Words.Include(w => w.Definitions).FirstOrDefaultAsync(entity => entity.Id == id);
            if (word == null)
            {
                return NotFound();
            }
            return Ok(word);
        }

        [HttpPost]
        public async Task<ActionResult<WordEntry>> PostWord([FromBody] WordEntry word)
        {
            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetWord",
                new { id = word.Id },
                word
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWord([FromRoute] int id, [FromBody] WordEntry word)
        {
            if (id != word.Id)
            {
                return BadRequest();
            }
            _context.Entry(word).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                if (_context.Words.Find(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WordEntry>> DeleteWord(int id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            _context.Words.Remove(word);
            await _context.SaveChangesAsync();
            return word;
        }
    }
}
