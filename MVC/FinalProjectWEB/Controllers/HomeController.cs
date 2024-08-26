using FinalProjectWEB.Data;
using FinalProjectWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalProjectWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpService _service;

        public HomeController(ILogger<HomeController> logger, IHttpService ies)
        {
            _logger = logger;
            _service = ies;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
