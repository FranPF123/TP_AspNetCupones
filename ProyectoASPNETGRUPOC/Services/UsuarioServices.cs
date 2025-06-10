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

        public async Task<List<Usuario>> GetUsuarios()
        {
            var ListaUsuarios = await _context.Usuarios.Where(u => u.Estado == true).ToListAsync();
            if (!ListaUsuarios.Any()) throw new KeyNotFoundException("Lista de Usuarios vacia");

            return ListaUsuarios;
        }

        public async Task<List<Usuario>> GetUsuariosPorRol(int id_Rol)
        {
            var ListaUsuariosPorRol = await _context.Usuarios.Include(r => r.Rol).Where(r => r.Rol.Id_Rol == id_Rol).OrderByDescending(u => u.User_Name).ToListAsync();

            if (!ListaUsuariosPorRol.Any()) throw new KeyNotFoundException("No existe usuarios con este Rol");

            return ListaUsuariosPorRol;
        }

        public async Task<Usuario> LoginUsuario(LoginModel login)
        {


            var Usuario = await _context.Usuarios.Include(u => u.Rol)
                .Where(u => u.User_Name == login.UserName && u.Password == login.Password)
                .FirstOrDefaultAsync();
            if (Usuario == null) throw new KeyNotFoundException("Credenciales invalidas");


            return Usuario;
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

            Usuario user = new Usuario()
            {
                User_Name = DtoUsuario.UserName,
                Password = DtoUsuario.Password,
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

    }
}
