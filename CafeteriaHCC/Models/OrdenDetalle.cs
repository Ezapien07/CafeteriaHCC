using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_HccOrdenesDetalle")]
    public class OrdenDetalle
    {
        [Key]
        [Column("orddet_id")]
        public int id {  get; set; }
        [Column("ord_id")]
        public int ordenId { get; set; }
        [Column("pro_id")]
        public int proId {  get; set; }
        [Column("orddet_cantidad")]
        public short cantidad { get; set; }
        [Column("orddet_estatus")]
        public byte estatus { get; set; }

    }
}
