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

namespace ELA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ELAContext _context;
        private readonly IUsuarioValidacao usuarioValidacao;

        public UsuariosController(ELAContext context, IUsuarioValidacao usuarioValidacao)
        {
            _context = context;
            this.usuarioValidacao = usuarioValidacao;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
                return NotFound();

            return Ok(await _context.Usuarios.ToListAsync());
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            if (_context.Usuarios == null)
                return NotFound();

            var usuario = usuarioValidacao.RetornarUsuario(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUsuario(Usuario usuario)
        {
            try
            {
                if (usuario.Id == null)
                    return BadRequest();

                var newData = new DateTime();
                newData = usuario.DataNascimento.Date;
                usuario.DataNascimento = newData;

                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRequest usuarioRequest)
        {
            try
            {
                if (_context.Usuarios == null)
                    return Problem("Entity set 'ELAContext.Usuarios' is null.");

                var usuario = usuarioValidacao.PostUsuario(usuarioRequest);

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok(usuario);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                if (_context.Usuarios == null)
                    return NotFound();

                var usuario = usuarioValidacao.RetornarUsuario(id);

                if (usuario == null)
                    return NotFound();

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            try
            {
                if (_context.Usuarios == null)
                {
                    return Problem("Entity set 'ELAContext.Usuarios' is null.");
                }

                await usuarioValidacao.Login(loginRequest);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
