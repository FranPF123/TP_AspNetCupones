using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Interfaces
{
    public interface IArticuloService
    {
        Task<Articulos> PostArticulo(DtoArticulo dtoArticulo);
        Task<Articulos> PutArticulo(int id, DtoArticulo dtoArticulo);
        Task<Articulos> DeleteArticulo(int id);
        Task<List<Articulos>> GetArticulos();
    }
}
