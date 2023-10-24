using ELA.Models;
using ELA.Models.Config;
using ELA.Validacoes;
using ELA.Validacoes.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ELA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiqueAtentoController : ControllerBase
    {
        private readonly MorusContext context;
        private readonly IPerguntaValidacao perguntaValidacao;

        public FiqueAtentoController(MorusContext context, IPerguntaValidacao perguntaValidacao)
        {
            this.context = context;
            this.perguntaValidacao = perguntaValidacao;
        }


        [HttpPost]
        public async Task<ActionResult<FiqueAtento>> PostFiqueAtento(FiqueAtento fiqueAtento)
        {
            try
            {
                if (context.FiqueAtentos == null)
                {
                    return Problem("Entity set 'MorusContext.FiqueAtento'  is null.");
                }

                await context.FiqueAtentos.AddAsync(fiqueAtento);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetFiqueAtento", new { id = fiqueAtento.Id }, fiqueAtento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
