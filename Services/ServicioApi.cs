using WEBEVENTOS.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Linq;

namespace WEBEVENTOS.Services
{
    public class ServicioApi : IServicioApi
    {

        private static string Url = "https://localhost:7211/api/";
        
        public ServicioApi() 
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
        
        public async Task<string> EditarUsuario(string cedula, Usuario cliente)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(Url); 
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PutAsync("Usuarios/" + cedula, content);
            if(response.IsSuccessStatusCode)
            {
                var json_respoonse = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respoonse);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }
        public async Task<string> EditarEvento(int id, Evento ev)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(Url);
            var content = new StringContent(JsonConvert.SerializeObject(ev), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PutAsync("Eventoes/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                var json_respoonse = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respoonse);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }

        public async Task<string> EliminarUsuario(string cedula)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress=new Uri(Url);
            var response = await clienteHttp.DeleteAsync("Usuarios/" + cedula);
            if(response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }

        public async Task<string> GuardarUsuario(Usuario cliente)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(Url); 
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PostAsync("Usuarios", content);
            Console.WriteLine(response.ToString());
            if(response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }
        public async Task<string> GuardarEvento(Evento ev)
        {
            string httpsResponseCode = HttpStatusCode.BadRequest.ToString();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(Url);
            var content = new StringContent(JsonConvert.SerializeObject(ev), Encoding.UTF8, "application/json");
            var response = await clienteHttp.PostAsync("Eventoes", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                httpsResponseCode = resultado.httpResponseCode;
            }
            return httpsResponseCode;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            List<Usuario> clientes = new List<Usuario> ();
            HttpClient clienteHtttp = new HttpClient ();
            clienteHtttp.BaseAddress = new Uri (Url);
            var response = await clienteHtttp.GetAsync("Usuarios");
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync(); 
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                clientes = resultado.LUsuario;
            }
            return clientes;
        }
        public async Task<List<Evento>> ListarEventos()
        {
            List<Evento> ev = new List<Evento>();
            HttpClient clienteHtttp = new HttpClient();
            clienteHtttp.BaseAddress = new Uri(Url);
            var response = await clienteHtttp.GetAsync("Eventoes");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                ev = resultado.LEvento;
            }
            return ev;
        }
        public async Task<List<DatosVenta>> ListarVentas()
        {
            List<Venta> ven = new List<Venta>();
            List<DatosVenta> dv = new List<DatosVenta>();
            HttpClient clienteHtttp = new HttpClient();
            clienteHtttp.BaseAddress = new Uri(Url);
            var response = await clienteHtttp.GetAsync("Ventas/Lista");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                ven = resultado.LVenta;
                List<Evento> le = resultado.LEvento;
                List<Usuario> lu = resultado.LUsuario;
                foreach (var v in ven)
                {
                    DatosVenta dvv = new DatosVenta();
                    Evento e = le.ElementAt(0);
                    le.RemoveAt(0);
                    Usuario u = lu.ElementAt(0);
                    lu.RemoveAt(0);
                    dvv.Id = v.Id;
                    dvv.cantidad = v.cantidad;
                    dvv.fechaEvento = e.fecha;
                    dvv.fechaCompra = v.fecha;
                    dvv.titulo = e.titulo;
                    dvv.descripcion = e.descripcion;
                    dvv.ubicacion = e.ubicacion;
                    dvv.costo = e.costo;
                    dvv.nombre = u.nombre;
                    dvv.cedula = u.cedula;
                    dvv.mail = u.mail;
                    dvv.telefono = u.telefono;
                    dvv.edad = u.edad;
                    dvv.foto = u.foto;
                    dv.Add(dvv);
                }
                
                
            }
            return dv;
        }

        public async Task<Usuario> ObtenerUsuario(string cedula)
        {
            Usuario cliente1 = new Usuario ();
            HttpClient clienteHttp = new HttpClient ();
            clienteHttp.BaseAddress = new Uri (Url);
            var response = await clienteHttp.GetAsync("Usuarios/" + cedula);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi> (json_response);
                cliente1 = resultado.NUsuario;
            }
            return cliente1;
        }
        public async Task<Evento> ObtenerEvento(int id)
        {
            Evento ev = new Evento();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(Url);
            var response = await clienteHttp.GetAsync("Eventoes/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                ev = resultado.NEvento;
            }
            return ev;
        }
        public async Task<DatosVenta> ObtenerVenta(int id)
        {
            DatosVenta ven = new DatosVenta();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri(Url);
            var response = await clienteHttp.GetAsync("Ventas/Ven/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                Venta v = resultado.NVenta;
                Evento e = resultado.NEvento;
                Usuario u = resultado.NUsuario;
                ven.Id = v.Id;
                ven.cantidad = v.cantidad;
                ven.fechaEvento = e.fecha;
                ven.fechaCompra = v.fecha;
                ven.titulo = e.titulo;
                ven.descripcion = e.descripcion;
                ven.ubicacion = e.ubicacion;
                ven.costo = e.costo;
                ven.nombre = u.nombre;
                ven.cedula = u.cedula;
                ven.mail = u.mail;
                ven.telefono = u.telefono;
                ven.edad = u.edad;
                ven.foto = u.foto;
        
    }
            return ven;
        }
        public async Task<Usuario> LoginUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                
                Usuario nu = new Usuario();
                HttpClient clienteHttp = new HttpClient();
                clienteHttp.BaseAddress = new Uri(Url);
                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await clienteHttp.PostAsync("Access", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var json_response = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_response);
                    nu = resultado.NUsuario;
                    return nu;
                }
                else { return null; }
                
            }return null;
            
        }
    }
}
