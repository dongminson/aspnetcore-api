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
    public class DefinitionsController : ControllerBase
    {
        private readonly Models.WordContext _context;

        public DefinitionsController(WordContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDefinitions([FromQuery] QueryParameters queryParameters)
        {
            IQueryable<DefinitionEntry> definitions = _context.Definitions;
            definitions = definitions.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if (typeof(WordEntry).GetProperty(queryParameters.SortBy) != null)
                {
                    definitions = definitions.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }
            return Ok(await definitions.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDefinition(int id)
        {
            var definition = await _context.Definitions.FirstOrDefaultAsync(entity => entity.Id == id);
            if (definition == null)
            {
                return NotFound();
            }
            return Ok(definition);
        }

        [HttpPost]
        public async Task<ActionResult<DefinitionEntry>> PostDefinition([FromBody]DefinitionEntry definition)
        {
            _context.Definitions.Add(definition);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetDefinition",
                new { id = definition.Id },
                definition
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefinition([FromRoute] int id, [FromBody] DefinitionEntry definition)
        {
            if (id != definition.Id)
            {
                return BadRequest();
            }
            _context.Entry(definition).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Definitions.Find(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DefinitionEntry>> DeleteDefinition(int id)
        {
            var definition = await _context.Definitions.FindAsync(id);
            if (definition == null)
            {
                return NotFound();
            }
            _context.Definitions.Remove(definition);
            await _context.SaveChangesAsync();
            return definition;
        }
    }
}