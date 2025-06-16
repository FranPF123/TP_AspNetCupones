using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model
{
	[Table("Tipo_Cupon")]
	public class TipoCupon
	{
		[Key]
		[Column("Id_Tipo_Cupon")]
		public int IdTipoCupon { get; set; }

		public string Nombre { get; set; }
	}
}

