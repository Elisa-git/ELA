using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes.Interface;
using ELA.Models.Requests;
using AutoMapper;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artigo>>> GetArtigos()
        {
            if (_context.Artigos == null)
                return NotFound();

            return Ok(await _context.Artigos.Include(a => a.Assuntos).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artigo>> GetArtigo(int id)
        {
            if (_context.Artigos == null)
                return NotFound();
            
            var artigo = artigoValidacao.RetornarArtigo(id);

            if (artigo == null)
                return NotFound();

            return Ok(artigo);
        }

        [HttpPut]
        public async Task<IActionResult> PutArtigo(ArtigoPutRequest artigoPutRequest)
        {
            try
            {
                var artigo = artigoValidacao.ValidarAtualizacao(artigoPutRequest);

                _context.Entry(artigo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(artigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Artigo>> PostArtigo(ArtigoRequest artigoRequest)
        {
            try
            {
                if (_context.Artigos == null)
                    return Problem("Entity set 'MorusContext.Artigos'  is null.");

                var artigo = artigoValidacao.ValidarArtigo(artigoRequest);

                await _context.Artigos.AddAsync(artigo);
                await _context.SaveChangesAsync();

                return Ok(artigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtigo(int id)
        {
            try
            {
                if (_context.Artigos == null)
                    return NotFound();

                var artigo = artigoValidacao.RetornarArtigo(id);
            
                if (artigo == null)
                    return NotFound();

                _context.Artigos.Remove(artigo);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
