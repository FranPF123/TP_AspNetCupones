using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoASPNETGRUPOC.Model
{
    [Table("Cupones_Historial")]
    public class CuponesHistorial
    {
        public int Id_Cupon { get; set; }
        public string NroCupon { get; set; }

        public DateTime FechaUso { get; set; }

        public int Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("Id_Cupon")]
        public virtual Cupones Cupon { get; set; }
    }
}
