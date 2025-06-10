using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;

namespace ProyectoASPNETGRUPOC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		public readonly IUsuarioServices UServices;

		public UsuarioController(IUsuarioServices uServices)
		{
			UServices = uServices;
		}

        [HttpGet]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<Usuario> listaUsuarios = await UServices.GetUsuarios();


                return Ok(listaUsuarios);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("MiPerfil")]
		[Authorize]
		public async Task<IActionResult> miPerfil()
		{
			try
			{
				var id = User.FindFirst("Id")?.Value;
				if (id == null) return NotFound("Id NULL");
				int idUsuario = int.Parse(id);
				var UsuarioLogeado = await UServices.obtenerDatosDeUsuarioLogeado(idUsuario);

				return Ok(new
				{
					Mensaje = "Mi Perfil:",

					usuarioLogeado = UsuarioLogeado
				});
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
