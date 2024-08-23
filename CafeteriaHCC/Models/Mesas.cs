using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CafeteriaHCC.Models
{
    [Table("Tb_HccMesas")]
    public class Mesas
    {
        [Key]
        [Column("mes_id")]
        public int id { get; set; }
         [Column("mes_lugares")]
        public short lugares {  get; set; }
         [Column("mes_disponible")]
        public byte disponible {  get; set; }
         [Column("mes_estatus")]
        public byte estatus { get; set; }
    }
}
