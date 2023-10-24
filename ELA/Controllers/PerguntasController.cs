﻿using ELA.Models;
using ELA.Models.Config;
using ELA.Models.Requests;
using ELA.Validacoes;
using ELA.Validacoes.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerguntasController : ControllerBase
    {
        private readonly MorusContext context;
        private readonly IPerguntaValidacao perguntaValidacao;

        public PerguntasController(MorusContext context, IPerguntaValidacao perguntaValidacao)
        {
            this.context = context;
            this.perguntaValidacao = perguntaValidacao;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pergunta>>> GetPerguntas()
        {
            if (context.Perguntas == null)
            {
                return NotFound();
            }
            return await context.Perguntas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pergunta>> GetPergunta(int id)
        {
            if (context.Perguntas == null)
            {
                return NotFound();
            }
            var pergunta = await context.Perguntas.FindAsync(id);

            if (pergunta == null)
            {
                return NotFound();
            }

            return pergunta;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPergunta(int id, Pergunta pergunta)
        {
            if (id != pergunta.Id)
            {
                return BadRequest();
            }

            context.Entry(pergunta).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!perguntaValidacao.PerguntaExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Pergunta>> PostPergunta(PerguntaRequest perguntaRequest)
        {
            try
            {
                if (context.Perguntas == null)
                {
                    return Problem("Entity set 'MorusContext.Perguntas'  is null.");
                }

                var pergunta = perguntaValidacao.ValidarPergunta(perguntaRequest);

                await context.Perguntas.AddAsync(pergunta);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetPergunta", new { id = pergunta.Id }, pergunta);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePergunta(int id)
        {
            if (context.Perguntas == null)
            {
                return NotFound();
            }
            var pergunta = await context.Perguntas.FindAsync(id);
            if (pergunta == null)
            {
                return NotFound();
            }

            context.Perguntas.Remove(pergunta);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}