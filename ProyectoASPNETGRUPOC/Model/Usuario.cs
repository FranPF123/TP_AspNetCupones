using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProyectoASPNETGRUPOC.Model
{
	[Table("Usuarios")]
	public class Usuario
	{
		[Key]
		[Column("Id_Usuario")]
		public int? id { get; set; }

		public string User_Name { get; set; }
		public string Password { get; set; }

		public string Nombre { get; set; }

		public string Apellido { get; set; }

		public string Dni { get; set; }

		public string Email { get; set; }
		public bool Estado { get; set; } = true;

		public int Id_Rol { get; set; } //Foreign key
		[ForeignKey("Id_Rol")]
		public virtual Roles? Rol { get; set; } //navegacion



	}
}
