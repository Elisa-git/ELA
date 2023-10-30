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
    public class AssuntosController : ControllerBase
    {
        private readonly MorusContext _context;
        private readonly IAssuntoValidacao assuntoValidacao;
        private readonly IMapper mapper;

        public AssuntosController(MorusContext context, IAssuntoValidacao assuntoValidacao, IMapper mapper)
        {
            _context = context;
            this.assuntoValidacao = assuntoValidacao;
            this.mapper = mapper;
        }

        // GET: api/Assuntos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assunto>>> GetAssuntos()
        {
            if (_context.Assuntos == null)
            {
                return NotFound();
            }

            return Ok(await _context.Assuntos.ToListAsync());
        }

        // GET: api/Assuntos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assunto>> GetAssunto(int id)
        {
            if (_context.Assuntos == null)
            {
                return NotFound();
            }

            var assunto = assuntoValidacao.RetornaAssunto(id);

            if (assunto == null)
            {
                return NotFound();
            }

            return Ok(assunto);
        }

        // PUT: api/Assuntos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAssunto(Assunto assunto)
        {
            try
            {
                if (assunto.Id == null)
                    throw new Exception("Id não informado");

                _context.Entry(assunto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(assunto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Assuntos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assunto>> PostAssunto(AssuntoRequest assuntoRequest)
        {
            try
            {
                if (_context.Assuntos == null)
                {
                    return Problem("Entity set 'MorusContext.Assuntos'  is null.");
                }

                var assunto = mapper.Map<Assunto>(assuntoRequest);
                _context.Assuntos.Add(assunto);
                await _context.SaveChangesAsync();

                return Ok(assunto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Assuntos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssunto(int id)
        {
            try
            {
                if (_context.Assuntos == null)
                    return NotFound();

                var assunto = assuntoValidacao.RetornaAssunto(id);
                                    
                if (assunto == null)
                    return NotFound();

                _context.Assuntos.Remove(assunto);
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
