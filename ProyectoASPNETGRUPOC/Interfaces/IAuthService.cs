using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginUsuario(LoginModel login);

        Task Registrar(DtoUsuario dtoUsuario);
    }
}
