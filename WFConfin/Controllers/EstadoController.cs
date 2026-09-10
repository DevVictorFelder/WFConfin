using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstadoController : Controller
    {

        private readonly WFConFinDbContext _context;

        public EstadoController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> GetEstados()
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
        public async Task <IActionResult> GetEstado([FromRoute] string sigla)
        {

            try
            {
                var estado = await _context.Estado.FindAsync(sigla);

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



        [HttpGet("Pesquisa")]
        public async Task <IActionResult> GetEstadoPesquisa([FromQuery] string valor)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Estado.ToList()
                            where o.Sigla.ToUpper().Contains(valor.ToUpper())
                            || o.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);

                /* 
                    select * from estado Where Upper(Sigla) like upper('%valo%') or Upper(nome) like ('%valor%')
                */

            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Estado. Exceção: {e.Message}");

            }


        }




        [HttpGet("Paginacao")]
        public async Task <IActionResult> GetEstadoPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Estado.ToList()
                            where o.Sigla.ToUpper().Contains(valor.ToUpper())
                            || o.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                if (ordemDesc)
                {
                    lista = from o in lista
                            orderby o.Nome descending
                            select o;
                }
                else
                {
                    lista = from o in lista
                            orderby o.Nome ascending
                            select o;
                }

                var qtde = lista.Count();

                lista = lista
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var paginacaoResponse = new PaginacaoResponse<Estado>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);

                /* 
                    select * from estado Where Upper(Sigla) like upper('%valo%') or Upper(nome) like ('%valor%')
                */

            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Estado. Exceção: {e.Message}");

            }


        }


        [HttpPost]
        [Authorize(Roles = "Gerente, Empregado")]
        public async Task <IActionResult> PostEstados([FromBody] Estado estado) // async e task
        {

            try
            {
               await _context.Estado.AddAsync(estado);// await

                var valor = await _context.SaveChangesAsync();
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
        [Authorize(Roles = "Gerente, Empregado")]
        public async Task <IActionResult> PutEstados([FromBody] Estado estado)
        {

            try
            {
                _context.Estado.Update(estado);

                var valor = await _context.SaveChangesAsync();
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
        [Authorize(Roles = "Gerente")]
        public async Task <IActionResult> DeleteEstados([FromRoute] string sigla)
        {

            try
            {
                var estado = await _context.Estado.FindAsync(sigla);

                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {
                    _context.Estado.Remove(estado);
                    var valor = await _context.SaveChangesAsync();
                    if(valor == 1)
                    {
                        return Ok("Estado Foi Removido com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Erro, Estado não removido!");
                    }
                    
                }

                else
                {
                    return NotFound("Erro, estado não existe.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro, estado não alterado. Exceção: {e.Message}");

            }


        }


    }
}
