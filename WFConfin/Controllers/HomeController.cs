using Microsoft.AspNetCore.Mvc;
using WFConfin.Models;

namespace WFConfin.Controllers
{

    [ApiController]// uma anotacao que indica que a classe abaixo é um controller de uma API, e que os metodos abaixo serao expostos como endpoints da API
    [Route("[controller]")]//uma anotacao que indica que a classe abaixo é um controller de uma API, e que os metodos abaixo serao expostos como endpoints da API, e que o caminho para acessar os metodos abaixo sera /home, pois o nome da classe é HomeController
    public class HomeController : Controller// uma anotacao que indica que a classe abaixo é um controller de uma API, e que os metodos abaixo serao expostos como endpoints da API, e que o caminho para acessar os metodos abaixo sera /home, pois o nome da classe é HomeController
    {

        private static List<Estado> listaEstados = new List<Estado>();//uma lista estatica de estados, que sera utilizada para armazenar os estados cadastrados na API

        [HttpGet("estado")]//uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é GetEstados
        public IActionResult GetEstados()//uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é GetEstados
        {
            return Ok(listaEstados);//uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é GetEstados
        }


        [HttpPost("estado")]//  uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é PostEstados
        public IActionResult PostEstados([FromBody] Estado estado)//uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é PostEstados, e que o parametro estado sera recebido no corpo da requisicao
        {
            listaEstados.Add(estado);//uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é PostEstados, e que o parametro estado sera recebido no corpo da requisicao
            return Ok("Estado cadastrado com sucesso!");// uma anotacao que indica que o metodo abaixo é um endpoint da API, e que o caminho para acessar o metodo abaixo sera /home/estado, pois o nome da classe é HomeController e o nome do metodo é PostEstados, e que o parametro estado sera recebido no corpo da requisicao
        }

    }
}
