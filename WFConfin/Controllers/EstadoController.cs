using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : Controller
    {

        private readonly WFConFinDbContext _context;

        public EstadoController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEstados()
        {

            try
            {
                var result = _context.Estado.ToList();

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro na listagem dos estados. Exceção: {e.Message}");

            }


        }

        [HttpGet("{sigla}")]
        public IActionResult GetEstado([FromRoute] string sigla)
        {

            try
            {
                var estado = _context.Estado.Find(sigla);

                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {
                    return Ok(estado);
                }

                else
                {
                    return NotFound("Erro, estado não existe.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Estado. Exceção: {e.Message}");

            }


        }

        [HttpPost]
        public IActionResult PostEstados([FromBody] Estado estado)
        {

            try
            {
                _context.Estado.Add(estado);

                var valor = _context.SaveChanges();
                if (valor == 1)
                {
                    return Ok("Sucesso, Estado incluido.");
                }
                else
                {
                    return BadRequest("Erro, Estado não incluido");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro, estado não incluido. Exceção: {e.Message}");

            }


        }

        [HttpPut]
        public IActionResult PutEstados([FromBody] Estado estado)
        {

            try
            {
                _context.Estado.Update(estado);

                var valor = _context.SaveChanges();
                if (valor == 1)
                {
                    return Ok("Sucesso, Estado alterado.");
                }
                else
                {
                    return BadRequest("Erro, Estado não alterado");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro, estado não alterado. Exceção: {e.Message}");

            }


        }


        [HttpDelete("{sigla}")]
        public IActionResult DeleteEstados([FromRoute] string sigla)
        {

            try
            {
                var estado = _context.Estado.Find(sigla);

                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {
                    _context.Estado.Remove(estado);
                    return Ok("Estado Foi Removido com sucesso!");
                }

                else
                {
                    return NotFound("Erro, estado não existe.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro, estado não incluido. Exceção: {e.Message}");

            }


        }


    }
}
