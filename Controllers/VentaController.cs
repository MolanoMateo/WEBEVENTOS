using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBEVENTOS.Models;
using WEBEVENTOS.Services;

namespace WEBEVENTOS.Controllers
{
    public class VentaController : Controller
    {
        private readonly IServicioApi _servicioApi;

        public VentaController(IServicioApi servicioapi)
        {
            _servicioApi = servicioapi;
        }
        // GET: VentaController
        public async Task<IActionResult> Index()
        {
            List<DatosVenta> clientes = new List<DatosVenta>();
            clientes = await _servicioApi.ListarVentas();
            return View(clientes);
        }

        public async Task<IActionResult> Details(int id)
        {
            DatosVenta cliente = new DatosVenta();
            cliente = await _servicioApi.ObtenerVenta(id);
            return View(cliente);
        }

    }
}
