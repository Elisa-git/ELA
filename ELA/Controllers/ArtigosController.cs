﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes.Interface;

namespace ELA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtigosController : ControllerBase
    {
        private readonly MorusContext _context;
        private readonly IArtigoValidacao artigoValidacao;

        public ArtigosController(MorusContext context, IArtigoValidacao artigoValidacao)
        {
            _context = context;
            this.artigoValidacao = artigoValidacao;
        }

        // GET: api/Artigos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artigo>>> GetArtigos()
        {
          if (_context.Artigos == null)
          {
              return NotFound();
          }
            return await _context.Artigos.ToListAsync();
        }

        // GET: api/Artigos/5
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

        // PUT: api/Artigos/5
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

        // POST: api/Artigos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artigo>> PostArtigo(Artigo artigo)
        {
            if (_context.Artigos == null)
            {
                return Problem("Entity set 'MorusContext.Artigos'  is null.");
            }

            artigoValidacao.ValidarArtigo(artigo);

            _context.Artigos.Add(artigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtigo", new { id = artigo.Id }, artigo);
        }

        // DELETE: api/Artigos/5
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
