using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model
{
    [Table("Cupones_Clientes")]
    public class CuponesClientes
    {
        [Key]
        [Column(Order = 0)]
        public int Id_Cupon { get; set; }

        [Required]
        public string NroCupon { get; set; }

        [Required]
        public DateTime FechaAsignado { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Id_Usuario { get; set; }

        // Relaciones de navegación
        [ForeignKey("Id_Cupon")]
        public virtual Cupones Cupon { get; set; }

        [ForeignKey("Id_Usuario")]
        public virtual Usuario Usuario { get; set; }

    }
}  
