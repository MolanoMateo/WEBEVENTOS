namespace WEBEVENTOS.Models
{
    public class DatosVenta
    {
        public int Id { get; set; }
        public int cantidad { get; set; }
        public String fechaEvento { get; set; }
        public String fechaCompra { get; set; }
        public String titulo { get; set; }
        public String descripcion { get; set; }
        public String ubicacion { get; set; }
        public float costo { get; set; }
        public String? cedula { get; set; }
        public String? nombre { get; set; }
        public String mail { get; set; }
        public String? telefono { get; set; }
        public int edad { get; set; }
        public String foto { get; set; }
    }
}
