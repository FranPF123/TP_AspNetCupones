using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model
{
	[Table("Roles")]
	public class Roles
	{
		[Key]
		[Column("Id_Rol")]
		public int Id_Rol { get; set; }
		public string Nombre { get; set; }

		public virtual ICollection<Usuario> Usuarios { get; set; }
	}
}
