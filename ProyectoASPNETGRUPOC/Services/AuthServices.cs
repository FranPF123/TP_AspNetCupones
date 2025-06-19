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
	public class AuthServices : IAuthServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration config;

		public AuthServices(ApplicationDbContext context, IConfiguration _config)
		{
			_context = context;
			config = _config;
		}
		public async Task<string> LoginUsuario(LoginModel login)
		{

			var Usuario = await _context.Usuarios.Include(u => u.Rol)
				.Where(u => u.User_Name == login.UserName)
				.FirstOrDefaultAsync();

			if (Usuario == null) throw new KeyNotFoundException("Credenciales invalidas");
			
			bool passwordCorrecta = BCrypt.Net.BCrypt.Verify(login.Password, Usuario.Password);
			
			if (!passwordCorrecta)
			{
				throw new KeyNotFoundException("Credenciales invalidas");
			}

            string Token = GenerarToken(Usuario);
            return Token;
        }

        public async Task Registrar(DtoUsuario dtoUsuario)
        {
            if (await ExisteUserConEmail(dtoUsuario.Email) || await ExiseUserName(dtoUsuario.UserName))
            {
                throw new KeyNotFoundException("Ya existe un Usuario con este Email o Correo.");
            }
			string passwordhHash = BCrypt.Net.BCrypt.HashPassword(dtoUsuario.Password);
            Usuario user = new Usuario()
            {
                User_Name = dtoUsuario.UserName,
                Password = passwordhHash,
                Nombre = dtoUsuario.Nombre,
                Apellido = dtoUsuario.Apellido,
                Dni = dtoUsuario.Dni,
                Email = dtoUsuario.Email,
                Id_Rol = dtoUsuario.Id_Rol, //Id del tipoUsuario por defecto;
            };


            _context.Usuarios.Add(user);

            await _context.SaveChangesAsync();
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


		private async Task<bool> ExisteUserConEmail(string Email)
		{
			var User = _context.Usuarios.Where(u => u.Email == Email).FirstOrDefault();
			if (User == null) return false;
			return true;
		}


		private async Task<bool> ExiseUserName(string UserName)
		{
			var User = _context.Usuarios.Where(u => u.User_Name == UserName).FirstOrDefault();

			if (User == null) return false;

			return true;
		}
	}
}
