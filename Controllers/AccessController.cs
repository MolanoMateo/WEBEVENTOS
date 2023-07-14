using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WEBEVENTOS.Models;
using WEBEVENTOS.Services;

namespace WEBEVENTOS.Controllers
{
    public class AccessController : Controller
    {
        private readonly IServicioApi _servicioApi;

        public AccessController(IServicioApi servicioapi)
        {
            _servicioApi = servicioapi;
        }

        // GET: AccessController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> login(Usuario _usuario)
        {
            Usuario us = await _servicioApi.LoginUsuario(_usuario);
            if (us != null)
            {
                var claims=new List<Claim> { new Claim(ClaimTypes.Name, us.nombre),
                new Claim("Correo", us.mail), new Claim(ClaimTypes.Role, us.rol)};
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("ErrorAcceso", "Access");
            }
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Access");
        }
        public ActionResult ErrorAcceso()
        {
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }

        // POST: AccessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: AccessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: AccessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
