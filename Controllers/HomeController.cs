using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBEVENTOS.Models;
using WEBEVENTOS.Services;
using Microsoft.AspNetCore.Authorization;

namespace WEBEVENTOS.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IServicioApi _servicioApi;

        public HomeController(IServicioApi servicioapi)
        {
            _servicioApi = servicioapi;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="admin,supervisor")]
        public IActionResult Eventos()
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