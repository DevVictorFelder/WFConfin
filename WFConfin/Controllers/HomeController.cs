using Microsoft.AspNetCore.Mvc;

namespace WFConfin.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult GetInformação()
        {

            var result = "Retorno em texto";
            return Ok(result);
        }
    }
}
