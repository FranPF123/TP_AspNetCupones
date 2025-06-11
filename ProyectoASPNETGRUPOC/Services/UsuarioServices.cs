using ProyectoASPNETGRUPOC.Data;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;
using Microsoft.EntityFrameworkCore;

namespace ProyectoASPNETGRUPOC.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;

        public UsuarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DtoUsuarioMuestra>> GetUsuarios()
        {
            var ListaUsuarios = await _context.Usuarios.Include(r => r.Rol)
                .Where(u => u.Estado == true).Select(u => new DtoUsuarioMuestra
                {
					UserName = u.User_Name,
					Password = u.Password,
					Nombre = u.Nombre,
					Apellido = u.Apellido,
					Dni = u.Dni,
					Email = u.Email,
					tipoUsuario = u.Rol.Nombre
				}).ToListAsync();

            if (!ListaUsuarios.Any()) throw new KeyNotFoundException("Lista de Usuarios vacia");

            return ListaUsuarios;
        }

        public async Task<List<DtoUsuarioMuestra>> GetUsuariosPorRol(int id_Rol)
        {
            var ListaUsuariosPorRol = await _context.Usuarios
                .Include(r => r.Rol)
                .Where(r => r.Rol.Id_Rol == id_Rol)
                .OrderByDescending(u => u.User_Name).Select(u => new DtoUsuarioMuestra
                {
                    UserName = u.User_Name,
                    Password = u.Password,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Dni = u.Dni,
                    Email = u.Email,
                    tipoUsuario = u.Rol.Nombre
                }).ToListAsync();

            if (!ListaUsuariosPorRol.Any()) throw new KeyNotFoundException("No existe usuarios con este Rol");

            return ListaUsuariosPorRol;
        }


        public async Task<DtoUsuarioMuestra> obtenerDatosDeUsuarioLogeado(int idUsuario)
        {
            var UsuarioLogeado = await _context.Usuarios.Include(u => u.Rol).Where(u => u.id == idUsuario).FirstOrDefaultAsync();

            if (UsuarioLogeado == null) throw new KeyNotFoundException("Error al tras los datos del usuario logeaod");



            DtoUsuarioMuestra dtoUsuarioMiPerfil = new DtoUsuarioMuestra()
            {
                UserName = UsuarioLogeado.User_Name,
                Password = UsuarioLogeado.Password,
                Nombre = UsuarioLogeado.Nombre,
                Apellido = UsuarioLogeado.Apellido,
                Dni = UsuarioLogeado.Dni,
                Email = UsuarioLogeado.Email,
                tipoUsuario = UsuarioLogeado.Rol.Nombre,
            };
            return dtoUsuarioMiPerfil;
        }

        public async Task PostCrearAdmin(DtoUsuario DtoUsuario)
        {
            if (ExisteUserConEmail(DtoUsuario.Email) || ExiseUserName(DtoUsuario.UserName))
            {
                throw new KeyNotFoundException("Ya existe un usuario con este correo.");
            }
			string passwordhHash = BCrypt.Net.BCrypt.HashPassword(DtoUsuario.Password);
			Usuario user = new Usuario()
            {
                User_Name = DtoUsuario.UserName,
                Password = passwordhHash,
                Nombre = DtoUsuario.Nombre,
                Apellido = DtoUsuario.Apellido,
                Dni = DtoUsuario.Dni,
                Email = DtoUsuario.Email,
                Id_Rol = DtoUsuario.Id_Rol,
            };


            _context.Usuarios.Add(user);

            await _context.SaveChangesAsync();

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

		public async Task EliminarUsuario(string user_name)
		{
			var Usuario = await BuscarPorUserName(user_name);

            Usuario.Estado = false;

            _context.Usuarios.Update(Usuario);

            await _context.SaveChangesAsync();  

		}

		public async Task EditarUsuario(DtoUsuario DtoUsuario, string user_name)
		{
            var UsuarioAEditar = await BuscarPorUserName(user_name);
            bool userNameCambio = DtoUsuario.UserName != UsuarioAEditar.User_Name;
            bool EmailCambio = DtoUsuario.Email != UsuarioAEditar.Email;
			
			if (userNameCambio)
            {
                bool userConEseUserName = ExiseUserName(DtoUsuario.UserName);

                if(userConEseUserName)
                {
                    throw new KeyNotFoundException("Ya existe un usuario con este user name");
                }
            }

            if (EmailCambio)
            {
                bool UserConEmail =  ExisteUserConEmail(DtoUsuario.Email);
                if (UserConEmail)
                {
                    throw new KeyNotFoundException("Ya existe un usuario con este email");
                }
            }
            if(DtoUsuario.Password != null)
            {
				bool passwordCorrecta = BCrypt.Net.BCrypt.Verify(DtoUsuario.Password, UsuarioAEditar.Password);
				if (!passwordCorrecta)
				{
					string passwordhHash = BCrypt.Net.BCrypt.HashPassword(DtoUsuario.Password);
					UsuarioAEditar.Password = passwordhHash;

				}
			}
			
            
			UsuarioAEditar.User_Name = DtoUsuario.UserName;
            UsuarioAEditar.Nombre = DtoUsuario.Nombre;
            UsuarioAEditar.Apellido = DtoUsuario.Apellido;
            UsuarioAEditar.Dni = DtoUsuario.Dni;
            UsuarioAEditar.Email = DtoUsuario.Email;

            _context.Usuarios.Update(UsuarioAEditar);

            await _context.SaveChangesAsync();




		}

		public async Task<Usuario> BuscarPorUserName(string user_name)
		{
            var Usuario = await _context.Usuarios.Where(u => u.User_Name == user_name).FirstOrDefaultAsync();

            if (Usuario == null) throw new KeyNotFoundException("No EXISTE un usuario con el user" + user_name);

            return Usuario;

		}
	}
}
