using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
    public class DtoUsuarioMuestra
    {
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string? tipoUsuario { get; set; }
    }
}
