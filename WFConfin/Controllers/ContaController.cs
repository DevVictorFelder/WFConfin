using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Threading.Tasks;
using WFConfin.Data;
using WFConfin.Models;

namespace WFConfin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : Controller
    {
        private readonly WFConFinDbContext _context;

        public ContaController(WFConFinDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetContas()
        {
            try
            {
                var result = _context.Conta.ToList();
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de Contas. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostConta([FromBody] Conta conta)
        {
            try
            {
                await _context.Conta.AddAsync(conta);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, Conta incluída.");
                }
                else
                {
                    return BadRequest("Erro, Conta não incluida");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel incuir Conta. Exceção: {e.Message} ");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutConta([FromBody] Conta conta)
        {
            try
            {
                _context.Conta.Update(conta);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, Conta alterada.");
                }
                else
                {
                    return BadRequest("Erro, conta não alterada");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel alterar conta. Exceção: {e.Message} ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta([FromRoute] Guid id)
        {
            try
            {
                Conta conta = await _context.Conta.FindAsync(id);
                if (conta != null)
                {
                    _context.Conta.Remove(conta);
                    var valor = await _context.SaveChangesAsync();
                    if (valor == 1)
                    {
                        return Ok("Sucesso, conta excluída");
                    }
                    else
                    {
                        return BadRequest("Erro, conta não excluída");
                    }
                }
                else
                {
                    return NotFound("Erro, conta não existe!");
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Erro, não foi possivel alterar conta. Exceção: {e.Message} ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConta([FromRoute] Guid id)
        {
            try
            {

                Conta conta = await _context.Conta.FindAsync(id);
                if (conta != null)
                {
                    return Ok(conta);
                }
                else
                {
                    return NotFound("Erro, na consulta da conta");
                }
            }

            catch (Exception e)
            {
                return BadRequest($"Erro na Exclusão de conta. Exceção: {e.Message}");
            }
        }

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetContaPesquisa([FromQuery] string valor)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Conta.Include(o => o.Pessoa).ToList()
                            where o.Descricao.ToUpper().Contains(valor.ToUpper())
                            || o.Pessoa.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                return Ok(lista);


            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de conta. Exceção: {e.Message}");

            }
        }

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetContaPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Conta.Include(o => o.Pessoa).ToList()
                            where o.Descricao.ToUpper().Contains(valor.ToUpper())
                            || o.Pessoa.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                if (ordemDesc)
                {
                    lista = from o in lista
                            orderby o.Descricao descending
                            select o;
                }
                else
                {
                    lista = from o in lista
                            orderby o.Descricao ascending
                            select o;
                }

                var qtde = lista.Count();

                lista = lista
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var paginacaoResponse = new PaginacaoResponse<Conta>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);

                /* 
                    select * from estado Where Upper(Sigla) like upper('%valo%') or Upper(nome) like ('%valor%')
                */

            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Conta. Exceção: {e.Message}");

            }


        }


        [HttpGet("Pessoa/{pessoaId}")]
        public async Task<IActionResult> GetContasPessoa([FromRoute]Guid pessoaId)
        {

            try
            {
                //Querry Criteria
                var lista = from o in _context.Conta.Include(o => o.Pessoa).ToList()
                            where o.PessoaId == pessoaId
                            select o;


                return Ok(lista);

                /* 
                    select * from estado Where Upper(Sigla) like upper('%valo%') or Upper(nome) like ('%valor%')
                */

            }
            catch (Exception e)
            {

                return BadRequest($"Erro, consulta de Conta por pessoa. Exceção: {e.Message}");

            }


        }

    }
}
