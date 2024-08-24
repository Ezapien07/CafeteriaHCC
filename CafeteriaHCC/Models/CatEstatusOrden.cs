using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_HccCatEstatusOrden")]
    public class CatEstatusOrden
    {
        [Key]
        [Column("catord_id")]
        public int id {  get; set; }
        [Column("catord_nombre")]
        public string nombre {  get; set; }
        [Column("catord_estatus")]
        public byte estatus {  get; set; }
    }
}
