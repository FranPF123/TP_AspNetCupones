using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

namespace ProyectoASPNETGRUPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        [Authorize(Policy = "ControlArticulos")]
        public async Task<IActionResult> GetArticulo()
        {
            try
            {
                List<Articulos> articulos = await _articuloService
                    .GetArticulos();
                return Ok(new
                {
                    Mensaje = "Lista de Articulos",
                    Articulos = articulos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ControlArticulos")]
        public async Task<IActionResult> PostArticulo(DtoArticulo dtoArticulo)
        {
            try
            {
                Articulos articulos = await _articuloService
                    .PostArticulo(dtoArticulo);

                return Ok(new
                {
                    Mensaje = "Se dio de alta el articulo",
                    articulos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Authorize(Policy = "ControlArticulos")]
        public async Task<IActionResult> PutArticulo(int id, DtoArticulo dtoArticulo)
        {
            try
            {
                var articulo = await _articuloService
                    .PutArticulo(id, dtoArticulo);

                return Ok($"Se modifico el Articulo con el id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ControlArticulos")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            try
            {
                var articulo = await _articuloService
                    .DeleteArticulo(id);
                return Ok(new
                {
                    Mensaje = "Se dio de baja el Articulo"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
