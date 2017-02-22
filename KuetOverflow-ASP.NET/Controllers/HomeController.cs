using Microsoft.AspNetCore.Mvc;


namespace KuetOverflow_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
