using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
        public IActionResult GetCidade()
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
        public IActionResult GetCidade([FromRoute] Guid id)
        {
            try
            {

                Cidade cidade = _context.Cidade.Find(id);
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
        public IActionResult PostCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Add(cidade);
                var valor = _context.SaveChanges();
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
        public IActionResult PutCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Update(cidade);
                var valor = _context.SaveChanges();
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
        public IActionResult DeleteCidade([FromRoute] Guid id)
        {
            try
            {

                Cidade cidade = _context.Cidade.Find(id);
                if (cidade != null)
                {
                    _context.Cidade.Remove(cidade);

                    _context.Cidade.Update(cidade);
                    var valor = _context.SaveChanges();
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

    }
}
