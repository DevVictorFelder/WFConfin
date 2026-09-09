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
    public class PessoaController : Controller
    {
        public readonly WFConFinDbContext _context;

        public PessoaController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                var result = _context.Pessoa.ToList();
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de pessoas. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                await _context.Pessoa.AddAsync(pessoa);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, Pessoa incluída.");
                }
                else
                {
                    return BadRequest("Erro, pessoa não incluida");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel incuir pessoa. Exceção: {e.Message} ");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                _context.Pessoa.Update(pessoa);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, Pessoa alterada.");
                }
                else
                {
                    return BadRequest("Erro, pessoa não alterada");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel alterar pessoa. Exceção: {e.Message} ");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa([FromRoute] Guid id)
        {
            try
            {
                Pessoa pessoa = await _context.Pessoa.FindAsync(id);
                if (pessoa != null)
                {
                    _context.Pessoa.Remove(pessoa);
                    var valor = await _context.SaveChangesAsync();
                    if (valor == 1)
                    {
                        return Ok("Sucesso, pessoa excluída");
                    }
                    else
                    {
                        return BadRequest("Erro, pessoa não excluída");
                    }
                }
                else
                {
                    return NotFound("Erro, Pessoa não existe!");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel alterar pessoa. Exceção: {e.Message} ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPessoa([FromRoute] Guid id)
        {
            try
            {

                Pessoa pessoa = await _context.Pessoa.FindAsync(id);
                if (pessoa != null)
                {
                    return Ok(pessoa);
                }
                else
                {
                    return NotFound("Erro, na consulta da pessoa");
                }
            }

            catch (Exception e)
            {
                return BadRequest($"Erro na Exclusão de pessoa. Exceção: {e.Message}");
            }
        }

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetPessoaPesquisa([FromQuery] string valor)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Pessoa.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                            || o.Telefone.ToUpper().Contains(valor.ToUpper())
                            || o.Email.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);


            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Pessoa. Exceção: {e.Message}");

            }
        }

            [HttpGet("Paginacao")]
            public async Task<IActionResult> GetPessoaPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
            {

                try
                {
                    //Querry Criteria
                    var lista = from o in _context.Pessoa.ToList()
                                where o.Nome.ToUpper().Contains(valor.ToUpper())
                                || o.Telefone.ToUpper().Contains(valor.ToUpper())
                                || o.Email.ToUpper().Contains(valor.ToUpper())
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

                    var paginacaoResponse = new PaginacaoResponse<Pessoa>(lista, qtde, skip, take);

                    return Ok(paginacaoResponse);

                    /* 
                        select * from estado Where Upper(Sigla) like upper('%valo%') or Upper(nome) like ('%valor%')
                    */

                }
                catch (Exception e)
                {

                    return BadRequest($"Erro, consulta de Pessoa. Exceção: {e.Message}");

                }


            }
        }

    }
