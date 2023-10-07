using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELA.Models;
using ELA.Models.Config;

namespace ELA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtigoesController : ControllerBase
    {
        private readonly MorusContext _context;

        public ArtigoesController(MorusContext context)
        {
            _context = context;
        }

        // GET: api/Artigoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artigo>>> GetArtigos()
        {
          if (_context.Artigos == null)
          {
              return NotFound();
          }
            return await _context.Artigos.ToListAsync();
        }

        // GET: api/Artigoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artigo>> GetArtigo(int id)
        {
          if (_context.Artigos == null)
          {
              return NotFound();
          }
            var artigo = await _context.Artigos.FindAsync(id);

            if (artigo == null)
            {
                return NotFound();
            }

            return artigo;
        }

        // PUT: api/Artigoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtigo(int id, Artigo artigo)
        {
            if (id != artigo.Id)
            {
                return BadRequest();
            }

            _context.Entry(artigo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtigoExists(id))
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

        // POST: api/Artigoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artigo>> PostArtigo(Artigo artigo)
        {
          if (_context.Artigos == null)
          {
              return Problem("Entity set 'MorusContext.Artigos'  is null.");
          }
            _context.Artigos.Add(artigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtigo", new { id = artigo.Id }, artigo);
        }

        // DELETE: api/Artigoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtigo(int id)
        {
            if (_context.Artigos == null)
            {
                return NotFound();
            }
            var artigo = await _context.Artigos.FindAsync(id);
            if (artigo == null)
            {
                return NotFound();
            }

            _context.Artigos.Remove(artigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtigoExists(int id)
        {
            return (_context.Artigos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
