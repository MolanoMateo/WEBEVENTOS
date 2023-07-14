using WEBEVENTOS.Models;

namespace WEBEVENTOS.Services
{
    public interface IServicioApi
    {
        Task<List<Usuario>> ListarUsuarios();
        Task<List<Evento>> ListarEventos();
        Task<List<DatosVenta>> ListarVentas();
        Task<Usuario> ObtenerUsuario(string cedula);
        Task<Evento> ObtenerEvento(int id);
        Task<DatosVenta> ObtenerVenta(int id);
        Task<Usuario> LoginUsuario(Usuario usuario);
        Task<string> GuardarUsuario(Usuario cliente);
        Task<string> GuardarEvento(Evento ev);
        Task<string> EditarUsuario(string cedula, Usuario cliente);
        Task<string> EditarEvento(int id, Evento ev);
        Task<string> EliminarUsuario(string cedula);
    }
}
