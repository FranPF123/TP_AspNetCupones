using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoASPNETGRUPOC.Model
{
	[Table("Cupones_Detalle")]
	public class CuponesDetalle
	{

		public int Id_Cupon { get; set; }

		public int Id_Articulo { get; set; }
		public int cantidad { get; set; }


		public virtual Cupones Cupon { get; set; }
		public virtual Articulos Articulos { get; set; }

	}
}
