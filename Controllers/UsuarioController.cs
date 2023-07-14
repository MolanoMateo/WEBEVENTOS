using Microsoft.AspNetCore.Http;

using WEBEVENTOS.Models;
using Microsoft.AspNetCore.Mvc;
using WEBEVENTOS.Services;

namespace WEBEVENTOS.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServicioApi _servicioApi;

        public UsuarioController(IServicioApi servicioapi)
        {
            _servicioApi = servicioapi;
        }
        // GET: UsuarioController
        public async Task<IActionResult> Index()
        {
            List<Usuario> clientes = new List<Usuario>();
            clientes = await _servicioApi.ListarUsuarios();
            return View(clientes);
        }

        // GET: UsuarioController/Details/5
        public async Task<IActionResult> Details(string cedula)
        {
            Usuario cliente = new Usuario();
            cliente = await _servicioApi.ObtenerUsuario(cedula);
            return View(cliente);
        }
        public async Task<IActionResult> Editar(string cedula)
        {
            Usuario cliente = await _servicioApi.ObtenerUsuario(cedula);
            if (cliente.nombre == " ")
            {
                cliente.nombre = null;
            }
            if (cliente.foto == " ")
            {
                cliente.foto = null;
            }
            return View(cliente);
        }
        public async Task<IActionResult> Edit(string cedula, Usuario us)
        {
            Usuario pend = await _servicioApi.ObtenerUsuario(cedula);
                if (us.nombre == null)
                {
                    us.nombre = " ";
                }
                if (us.edad == null)
                {
                    us.edad = 0;
                }
                if (us.telefono == null)
                {
                    us.telefono = " ";
                }
                if (us.foto == null)
                {
                    us.foto = " ";
                }
                if (us.password == null)
                {
                    us.password = pend.password;
                }
                await _servicioApi.EditarUsuario(cedula, us);
                return RedirectToAction("Index");
            
        }
        // GET: UsuarioController/Create
        public ActionResult Crear()
        {
            Usuario us = new Usuario();
            return View(us);
        }
        public async Task<IActionResult> save(Usuario us)
        {
            if (us.cedula == null || us.rol == null)
            {
                return null;
            }
            else
            {
                if(us.nombre == null)
                {
                    us.nombre = " ";
                }
                if(us.edad == null){
                    us.edad = 0;
                }
                if (us.telefono == null)
                {
                    us.telefono = " ";
                }
                if (us.foto == null)
                {
                    us.foto = " ";
                }
                await _servicioApi.GuardarUsuario(us);
                return RedirectToAction("Index");
            }
            
        }



        public async Task<IActionResult> Eliminar(string cedula)
        {
            Usuario cliente = await _servicioApi.ObtenerUsuario(cedula);
            await _servicioApi.EliminarUsuario(cliente.cedula);
            //return RedirectToAction("Index");
            return View(cliente);

        }

        public async Task<IActionResult> Delete(Usuario us)
        {
            await _servicioApi.EliminarUsuario(us.cedula);
            return RedirectToAction("Index");
        }
    }
}
