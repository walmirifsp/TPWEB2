using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TPFinal.Services;
using TPFinal.Models;

namespace TPFinal.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        DevelopersContext _context;
        public UsuarioController(ILogger<UsuarioController> logger, DevelopersContext context)
        {
            _logger = logger;
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Usuario>> index()
        {
            try
            {
                IAsyncEnumerable<Usuario> usuariosList = new UsuarioService(_context).GetList();
                var usuarios = new List<Usuario>();
                await foreach (Usuario usuario in usuariosList)
                {
                    usuarios.Add(usuario);
                }
                return Json(usuarios);
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - Database error" }));
            }
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Usuario>> getById(int id)
        {
            try
            {
                Usuario usuario = await new UsuarioService(_context).FetchById(id);
                if (usuario == null)
                    throw new Exception();
                return usuario;
            }
            catch
            {
                return NotFound(
                  Json(new { error = $"Not Found - id {id}" }));

            }
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> Add([FromBody] Usuario usuario)
        {
            try
            {
                await new UsuarioService(_context).Add(usuario);
                return usuario;
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - Database error" }));
            }
        }

        [HttpPost("session")]
        public async Task<ActionResult<Usuario>> Session([FromBody] Usuario usuario)
        {
           
            try
            {
                var user = await new UsuarioService(_context).Session(usuario.Nome, usuario.Senha);
                return user;
            }
            catch
            {
                return StatusCode(401, Json(new { error = "Usuário e/ou senha inválidos" }));
            }
        }

        [HttpPut]
        public async Task<ActionResult<Usuario>> Update([FromBody] Usuario usuario)
        {
            try
            {
                await new UsuarioService(_context).Update(usuario);
                return usuario;
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - Database error" }));
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Usuario>> Remove(int usuarioId)
        {

            if (usuarioId == 0)
                return BadRequest();

            try
            {
                await new UsuarioService(_context).Remove(usuarioId);
                return NoContent();
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - remove fails" }));
            }
        }
    }
}