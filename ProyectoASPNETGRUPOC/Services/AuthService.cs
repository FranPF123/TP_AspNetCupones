using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoASPNETGRUPOC.Data;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoASPNETGRUPOC.Services
{
	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration config;

		public AuthService(ApplicationDbContext context, IConfiguration _config)
		{
			_context = context;
			config = _config;
		}
		public Task<string> LoginUsuario(LoginModel login)
		{
			throw new NotImplementedException();
		}

		public Task Registrar(DtoUsuario dtoUsuario)
		{
			throw new NotImplementedException();
		}


		private string GenerarToken(Usuario usuarioEntity)
		{
			var claims = new[]
			{
					new Claim("Id", usuarioEntity.id.ToString()),
					new Claim(ClaimTypes.Name, usuarioEntity.User_Name),
					new Claim(ClaimTypes.Role, usuarioEntity.Rol.Nombre)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds

			);
			return new JwtSecurityTokenHandler().WriteToken(token);

		}


		private bool ExisteUserConEmail(string Email)
		{
			var User = _context.Usuarios.Where(u => u.Email == Email).FirstOrDefault();
			if (User == null) return false;
			return true;
		}


		private bool ExiseUserName(string UserName)
		{
			var User = _context.Usuarios.Where(u => u.User_Name == UserName).FirstOrDefault();

			if (User == null) return false;

			return true;
		}
	}
}
