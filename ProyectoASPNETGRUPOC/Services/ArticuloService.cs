﻿using Microsoft.EntityFrameworkCore;
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
                    .Where(a => a.Activo == true)
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
                Articulos? articuloEntity = await _context.Articulos
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

		public async Task<bool> existeArticuloId(int idArticulo)
		{
			var ArticuloPorId = await _context.Articulos.Where(a => a.Id_Articulo == idArticulo && a.Activo == true).FirstOrDefaultAsync();

			if (ArticuloPorId == null) return false;

			return true;
		}

		public async Task<bool> precioDeArticulo(int idArticulo)
		{
			var ArticuloPrecio = await _context.Articulos.Where(a => a.Id_Articulo == idArticulo).FirstOrDefaultAsync();

            if (ArticuloPrecio.Precio <= 0) return true;

            return false;

		}

        public async Task<List<DtoReporteArticulos>> ArticulosMasUsados()
        {
            var reporte = await _context.Cupones_Detalle
                .GroupBy(cd => cd.Id_Articulo)
                .Select(g => new DtoReporteArticulos
                {
                    Id_Articulo = g.Key,
                    Nombre = _context.Articulos
                        .Where(a => a.Id_Articulo == g.Key)
                        .Select(a => a.Nombre_articulo)
                        .FirstOrDefault(),
                    Cantidad_Usos = g.Sum(x => x.cantidad)
                })
                .OrderByDescending(x => x.Cantidad_Usos)
                .ToListAsync();

            return reporte;
        }

    }
}
