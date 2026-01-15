using System.Diagnostics;
using cctvtemplate.Models;
using Microsoft.AspNetCore.Mvc;

namespace cctvtemplate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
