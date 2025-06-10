using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IUsuarioServices
    {
        Task<List<Usuario>> GetUsuarios();
        Task<List<DtoUsuarioMuestra>> GetUsuariosPorRol(int id_Rol);
        Task PostCrearAdmin(DtoUsuario DtoUsuario);


        Task<DtoUsuarioMuestra> obtenerDatosDeUsuarioLogeado(int idUsuario);
    }
}
