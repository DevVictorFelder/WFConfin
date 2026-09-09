using Microsoft.AspNetCore.Mvc;

namespace WFConfin.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
