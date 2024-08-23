using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CafeteriaHCC.Models
{
    [Table("Tb_HccOrdenes")]
    public class Orden
    {
        [Key]
        [Column("ord_id")] 
        public int id {  get; set; }
        [Column("mes_id")] 
        public int mesaId { get; set; }
        [Column("catord_id")]
        public int catOrdId {  get; set; }
        [Column("ord_fecha_inicio")]
        public DateTime fechaInicio { get; set; }
        [Column("ord_estatus")]
        public byte estatus { get; set; }

        //public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; }
    }
    public class Detalles
    {
        public int idProducto { get; set; }
        public short cantidad { get; set; }
    }
}
