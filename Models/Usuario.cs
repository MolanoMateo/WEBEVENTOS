using System.ComponentModel.DataAnnotations;
namespace WEBEVENTOS.Models
{
    public class Usuario
    {
        [Key]
        public String? cedula { get; set; }
        public String? nombre { get; set; }
        public String mail { get; set; }
        public String? telefono { get; set; }
        public int edad { get; set; }
        public String? foto { get; set; }
        public string password { get; set; }
        public string? rol { get; set; }
    }
}
