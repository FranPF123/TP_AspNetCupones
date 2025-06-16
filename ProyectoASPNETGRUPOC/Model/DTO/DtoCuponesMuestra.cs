using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
	public class DtoCuponesMuestra
	{
        public int Id_Cupon { get; set; }
        [Required]
		public string Nombre { get; set; }
		[Required]
		public string Descripcion { get; set; }
		[Required]
		public decimal PorcentajeDto { get; set; }
		[Required]
		public decimal ImportePromo { get; set; }
		[Required]
		public DateTime FechaInicio { get; set; }
		[Required]
		public DateTime FechaFinal {  get; set; }
		[Required]
		public string NombreTipoCupon { get; set; }
        public string Estado { get; set; }
    }
}
