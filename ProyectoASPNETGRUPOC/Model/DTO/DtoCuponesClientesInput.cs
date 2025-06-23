using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
	public class DtoCuponesClientesInput
	{
		[Required]
		public int Id_Cupon { get; set; }


		[Required]
		public int Id_Usuario { get; set; }
		[Required]
		public string NroCupon { get; set; }

		[Required]
		public DateTime FechaAsignado { get; set; }
	}
}
