using Microsoft.AspNetCore.Mvc;

namespace cctvtemplate.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
