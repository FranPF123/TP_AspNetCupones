using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model
{
	public class Articulos
	{
		[Key]
		public int Id_Articulo { get; set; }

		public string Nombre_articulo { get; set; }

		public string Descripcion_articulo { get; set; }

		public bool Activo { get; set; } = true;

		public decimal Precio { get; set; }
		[ForeignKey("Id_Articulo")]
		public ICollection<CuponesDetalle> CuponesDetalles { get; set; }
	}
}
