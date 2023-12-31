﻿using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes;
using ELA.Validacoes.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiqueAtentoController : ControllerBase
    {
        private readonly ELAContext context;
        private readonly IFiqueAtentoValidacao fiqueAtentoValidacao;

        public FiqueAtentoController(ELAContext context, IFiqueAtentoValidacao fiqueAtentoValidacao)
        {
            this.context = context;
            this.fiqueAtentoValidacao = fiqueAtentoValidacao;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FiqueAtento>>> GetFiqueAtento()
        {
            if (context.FiqueAtentos == null)
                return NotFound();

            return Ok(await context.FiqueAtentos.Include(a => a.Assuntos).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FiqueAtento>> GetFiqueAtento(int id)
        {
            if (context.FiqueAtentos == null)
                return NotFound();

            var fiqueAtento = fiqueAtentoValidacao.RetornarFiqueAtento(id);

            if (fiqueAtento == null)
                return NotFound();

            return Ok(fiqueAtento);
        }

        [HttpPut]
        public async Task<IActionResult> PutFiqueAtento(FiqueAtentoPutRequest fiqueAtentoPutRequest)
        {
            try
            {
                var fiqueAtento = fiqueAtentoValidacao.ValidarAtualizacao(fiqueAtentoPutRequest);

                context.Entry(fiqueAtento).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(fiqueAtento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<ActionResult<FiqueAtento>> PostFiqueAtento(FiqueAtentoRequest fiqueAtentoRequest)
        {
            try
            {
                if (context.FiqueAtentos == null)
                    return Problem("Entity set 'ELAContext.FiqueAtento'  is null.");

                var fiqueAtento = fiqueAtentoValidacao.ValidarFiqueAtento(fiqueAtentoRequest);
                    
                await context.FiqueAtentos.AddAsync(fiqueAtento);
                await context.SaveChangesAsync();

                return Ok(fiqueAtento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiqueAtento(int id)
        {
            try
            {
                if (context.FiqueAtentos == null)
                    return NotFound();

                var fiqueAtento = fiqueAtentoValidacao.RetornarFiqueAtento(id);

                if (fiqueAtento == null)
                    return NotFound();

                context.FiqueAtentos.Remove(fiqueAtento);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
