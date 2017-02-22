using Microsoft.AspNetCore.Mvc;


namespace KuetOverflow_ASP.NET.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
