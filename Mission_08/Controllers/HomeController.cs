using Microsoft.AspNetCore.Mvc;

namespace Mission_08Team3_10.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
