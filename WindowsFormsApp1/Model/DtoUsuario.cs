using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
	public class DtoUsuario
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Dni { get; set; }
		public string Email { get; set; }
		public int Id_Rol { get; set; } = 2;
	}
}
