using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WFConfin.Data;
using WFConfin.Models;
using WFConfin.Services;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly WFConFinDbContext _context;
        private readonly TokenService _service;

        public UsuarioController(WFConFinDbContext context, TokenService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuariologin)
        {
            var usuario = _context.Usuario.Where(x => x.Login == usuariologin.Login).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound("Usuário Inválido!");
            }
            if (usuario.Password != usuariologin.Password)
            {
                return BadRequest("Senha inválida");
            }

            var token = _service.GerarToken(usuario);
            usuario.Password = "";
            var result = new UsuarioResponse()
            {
                Usuario = usuario,
                Token = token
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            try
            {
                var result = _context.Usuario.ToList();
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest($"Erro na Listagem de Usuários. Excecão: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var listUsuario = _context.Usuario.Where(o => o.Login == usuario.Login).ToList();
                if(listUsuario.Count > 0)
                {
                    return BadRequest("Erro, Informação invalida de login.");
                }

                await _context.Usuario.AddAsync(usuario);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, Usuario incluída.");
                }
                else
                {
                    return BadRequest("Erro, Usuario não incluida");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel incuir Usuario. Exceção: {e.Message} ");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, usuario alterada.");
                }
                else
                {
                    return BadRequest("Erro, usuario não alterada");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel alterar Usuario. Exceção: {e.Message} ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] Guid id)
        {
            try
            {
                Usuario usuario = await _context.Usuario.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuario.Remove(usuario);
                    var valor = await _context.SaveChangesAsync();
                    if (valor == 1)
                    {
                        return Ok("Sucesso, usuario excluída");
                    }
                    else
                    {
                        return BadRequest("Erro, usuario não excluída");
                    }
                }
                else
                {
                    return NotFound("Erro, usuario não existe!");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel alterar usuario. Exceção: {e.Message} ");
            }
        }


    }
}
