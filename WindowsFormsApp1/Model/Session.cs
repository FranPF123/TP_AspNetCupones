using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
	public static class Session
	{
		public static string idUsuario {  get; set; }
		public static string User_Name {  get; set; }
		public static string Token {  get; set; }
		public static string Rol {  get; set; }
	}
}
