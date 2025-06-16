using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model
{
	public class Cupones
	{
		[Key]
		[Column("Id_Cupon")]
		public int Id_Cupon { get; set; }

		public string Nombre { get; set; }

		public string Descripcion { get; set; }

		public decimal PorcentajeDto { get; set; }

		public decimal ImportePromo { get; set; }

		public DateTime FechaInicio { get; set; } = DateTime.Now;

		public DateTime FechaFin { get; set; }

		public bool? Activo { get; set; } = true;
		public int Id_Tipo_Cupon { get; set; }



		[ForeignKey("Id_Tipo_Cupon")]
		public virtual TipoCupon? TipoCupon { get; set; }
		[ForeignKey("Id_Cupon")]
		public ICollection<CuponesDetalle>? CuponesDetalles { get; set; }
	}
}
