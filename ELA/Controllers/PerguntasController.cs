using ELA.Models;
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
            return Ok(await context.Perguntas.Include(a => a.Assuntos).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pergunta>> GetPergunta(int id)
        {
            if (context.Perguntas == null)
            {
                return NotFound();
            }

            var pergunta = perguntaValidacao.RetornarPergunta(id);

            if (pergunta == null)
            {
                return NotFound();
            }

            return Ok(pergunta);
        }

        [HttpPut]
        public async Task<IActionResult> PutPergunta(PerguntaPutRequest perguntaPutRequest)
        {
            try
            {
                var pergunta = perguntaValidacao.ValidarAtualizacao(perguntaPutRequest);

                context.Entry(pergunta).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(pergunta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

                return Ok(pergunta);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePergunta(int id)
        {
            try 
            { 
                if (context.Perguntas == null)
                {
                    return NotFound();
                }
                var pergunta = perguntaValidacao.RetornarPergunta(id);
                if (pergunta == null)
                {
                    return NotFound();
                }

                context.Perguntas.Remove(pergunta);
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
