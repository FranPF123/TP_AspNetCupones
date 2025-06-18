using Microsoft.EntityFrameworkCore;
using ProyectoASPNETGRUPOC.Data;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly ApplicationDbContext _context;

        public ArticuloService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Articulos> DeleteArticulo(int id)
        {
            try
            {
                var articulo = await _context.Articulos.FindAsync(id);
                if (articulo == null || articulo.Activo == false)
                    throw new Exception("El Articulo indicado no existe o no fue encontrado.");

                articulo.Activo = false;

                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();

                return articulo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Articulos>> GetArticulos()
        {

            try
            {
                var articulos = await _context.Articulos
                    .Include(a => a.Activo == true)
                    .ToListAsync();

                if (articulos.Any())
                    return articulos;
                else
                    throw new Exception("No se encontraron Articulos.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Articulos> PostArticulo(DtoArticulo dtoArticulo)
        {
            try
            {
                Articulos articuloEntity = await _context.Articulos
                    .FirstOrDefaultAsync(a => a.Nombre_articulo == dtoArticulo.Nombre_articulo);

                if (articuloEntity is not null)
                {
                    throw new Exception("Ya existe el Articulo");
                }

                Articulos articulos = new Articulos()
                {
                    Nombre_articulo = dtoArticulo.Nombre_articulo,
                    Descripcion_articulo = dtoArticulo.Descripcion_articulo,
                    Precio = dtoArticulo.Precio,
                    Activo = true,
                };

                _context.Articulos.Add(articulos);
                await _context.SaveChangesAsync();
                return articulos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Articulos> PutArticulo(int id, DtoArticulo dtoArticulo)
        {
            try
            {
                var articulo = await _context.Articulos
                    .FirstOrDefaultAsync(a => a.Id_Articulo == id);
                
                if (articulo == null)
                {
                    throw new Exception("El Articulo indicado no existe");
                }

                articulo.Nombre_articulo = dtoArticulo.Nombre_articulo;
                articulo.Descripcion_articulo = dtoArticulo.Descripcion_articulo;
                articulo.Precio = dtoArticulo.Precio; 
                articulo.Activo = true;

                await _context.SaveChangesAsync();
                return articulo;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
