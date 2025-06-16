using ProyectoASPNETGRUPOC.Model.DTO;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IAuthServices
    {
        Task<string> LoginUsuario(LoginModel login);

        Task Registrar(DtoUsuario dtoUsuario);
    }
}
