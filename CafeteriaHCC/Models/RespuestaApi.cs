using System.Text.Json;

namespace CafeteriaHCC.Models
{
    public class RespuestaApi
    {
        public int Estatus { get; set; }
        public string Mensaje { get; set; }
        public int Codigo { get; set; }
    }
}

