using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WFConfin.Data;
using WFConfin.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;


namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : Controller
    {

        private readonly WFConFinDbContext _context;

        public CidadeController(WFConFinDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task <IActionResult> GetCidade()
        {
            try
            {
                var result = _context.Cidade.ToList();

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de cidades. Exceção: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCidade([FromRoute] Guid id)
        {
            try
            {

                Cidade cidade = await _context.Cidade.FindAsync(id);
                if (cidade != null)
                {
                    return Ok(cidade);
                }
                else
                {
                    return NotFound("Erro, na consulta da cidade");
                }
            }

            catch (Exception e)
            {
                return BadRequest($"Erro na Exclusão de cidade. Exceção: {e.Message}");
            }
        }



        [HttpPost]
        public async Task <IActionResult> PostCidade([FromBody] Cidade cidade)
        {
            try
            {
                await _context.Cidade.AddAsync(cidade);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, cidade incluida");
                }
                else
                {
                    return BadRequest("Erro, cidade nao incluida");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na inclusão de cidade. Exceção: {e.Message}");
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutCidade([FromBody] Cidade cidade)
        {
            try
            {
                 _context.Cidade.Update(cidade);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, cidade alterada");
                }
                else
                {
                    return BadRequest("Erro, cidade nao alterada");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na alteração de cidade. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteCidade([FromRoute] Guid id)
        {
            try
            {

                Cidade cidade = await _context.Cidade.FindAsync(id);
                if (cidade != null)
                {
                    _context.Cidade.Remove(cidade);

                    _context.Cidade.Update(cidade);
                    var valor = await _context.SaveChangesAsync();
                    if (valor == 1)
                    {
                        return Ok("Sucesso, cidade Excluida");
                    }
                    else
                    {
                        return BadRequest("Erro, cidade nao Excluida");
                    }
                }
                else
                {
                    return NotFound("Erro, cidade nao existe");
                }
            }

            catch (Exception e)
            {
                return BadRequest($"Erro na Exclusão de cidade. Exceção: {e.Message}");
            }
        }

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetCidadePesquisa([FromQuery] string valor)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Cidade.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            || o.EstadoSigla.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);


            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Cidade. Exceção: {e.Message}");

            }


        }

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetCidadePaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Cidade.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            || o.EstadoSigla.ToUpper().Contains(valor.ToUpper())
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

                var paginacaoResponse = new PaginacaoResponse<Cidade>(lista, qtde, skip, take);

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


    }
}
