using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model
{
	public class Cupones
	{
		[Key]
		[Column("Id_Cupon")]
		public int Id_Cupon { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Descripcion { get; set; }
		[Required]
		public decimal? PorcentajeDto { get; set; } = 0;
		[Required]
		public decimal? ImportePromo { get; set; } = 0;
		[Required]
		public DateTime FechaInicio { get; set; } = DateTime.Now;
		[Required]
		public DateTime FechaFin { get; set; }
		[Required]
		public bool? Activo { get; set; } = true;
		[Required]
		public int Id_Tipo_Cupon { get; set; }



		[ForeignKey("Id_Tipo_Cupon")]
		public virtual TipoCupon? TipoCupon { get; set; }
		[ForeignKey("Id_Cupon")]
		public ICollection<CuponesDetalle>? CuponesDetalles { get; set; }



	}
}
