using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WEBEVENTOS.Services;
using WEBEVENTOS.Models;

namespace WEBEVENTOS.Controllers
{
    public class EventoController : Controller
    {
        private readonly IServicioApi _servicioApi;

        public EventoController(IServicioApi servicioapi)
        {
            _servicioApi = servicioapi;
        }
        // GET: EventoController
        public async Task<IActionResult> Index()
        {
            List<Evento> ev = new List<Evento>();
            ev = await _servicioApi.ListarEventos();
            return View(ev);
        }
        public ActionResult Crear()
        {
            Evento ev = new Evento();
            return View(ev);
        }
        public async Task<IActionResult> save(Evento ev)
        {
            await _servicioApi.GuardarEvento(ev);
            return RedirectToAction("Index");
        }

        // GET: EventoController/Details/5
        public string GenerateDynamicSourceUrl()
        {
            string dynamicUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15959.199617575281!2d-78.47677915189375!3d-0.17408377383324325!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x91d59be093057017%3A0x74182a828f42a583!2sLa%20Quinta%20by%20Wyndham%20Quito!5e0!3m2!1ses-419!2sec!4v1688968690209!5m2!1ses-419!2sec";
            return dynamicUrl;//AIzaSyDv_HzEcJkJY8axaekux4b53aZLEjXYStU Install-Package Google.Apis.Drive.v3
        }

        // GET: EventoController/Create
        public ActionResult mapa(string map)
        {
            //Console.WriteLine(map);
            return RedirectToAction("Index", "Evento");
        }

        public async Task<IActionResult> Editar(int id)
        {
            Evento cliente = await _servicioApi.ObtenerEvento(id);
            return View(cliente);
        }
        public async Task<IActionResult> Update(Evento cliente)
        {
            await _servicioApi.EditarEvento(cliente.Id, cliente);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            Evento cliente = new Evento();
            cliente = await _servicioApi.ObtenerEvento(id);
            return View(cliente);
        }

    }
}
