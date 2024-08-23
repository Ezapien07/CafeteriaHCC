namespace CafeteriaHCC.Models
{
    public class Productos
    {
        public int id {  get; set; }
        public int almId {  get; set; }
        public string nombre {  get; set; }
        public string descripcion {  get; set; }
        public decimal precio {  get; set; }
        public decimal estatus {  get; set; }
    }
}
