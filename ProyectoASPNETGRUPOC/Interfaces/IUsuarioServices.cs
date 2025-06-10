using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IUsuarioServices
    {
        Task<List<Usuario>> GetUsuarios();
        Task<List<Usuario>> GetUsuariosPorRol(int id_Rol);
        Task PostCrearAdmin(DtoUsuario DtoUsuario);
        Task<Usuario> LoginUsuario(LoginModel login);
        //Task<bool> ExisteUserConEmail(string email);

        Task<DtoUsuarioMuestra> obtenerDatosDeUsuarioLogeado(int idUsuario);
    }
}
