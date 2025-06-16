using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
	public class DtoCupones
	{
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Descripcion { get; set; }
		[Required]
		public decimal PorcentajeDto { get; set; }
		[Required]
		public decimal ImportePromo { get; set; }

		public DateTime FechaInicio { get; set; } = DateTime.Now;
		[Required]

		public int DiasDuracionDeCupon { get; set; }
		public int Id_Tipo_Cupon { get; set; }


	}
}
