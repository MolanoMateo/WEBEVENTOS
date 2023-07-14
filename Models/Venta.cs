namespace WEBEVENTOS.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public String usuario { get; set; }
        public int idEvento { get; set; }
        public int cantidad { get; set; }
        public String fecha { get; set; }
    }
}
