using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IUsuarioServices
    {
        Task<List<DtoUsuarioMuestra>> GetUsuarios();
        Task<List<DtoUsuarioMuestra>> GetUsuariosPorRol(int id_Rol);
        Task PostCrearAdmin(DtoUsuario DtoUsuario);

        Task EliminarUsuario(string user_name);

        Task EditarUsuario(DtoUsuario DtoUsuario, string user_name);
        Task<DtoUsuarioMuestra> obtenerDatosDeUsuarioLogeado(int idUsuario);

        Task<Usuario> BuscarPorUserName(string user_name);
    }
}
