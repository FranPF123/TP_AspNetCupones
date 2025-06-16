using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoASPNETGRUPOC.Interfaces;
using ProyectoASPNETGRUPOC.Model;
using ProyectoASPNETGRUPOC.Model.DTO;

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
                List<DtoUsuarioMuestra> listaUsuarios = await UServices.GetUsuarios();


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
        [HttpGet("UsuariosSegunRol/")]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> GetUsuariosPorRol(int id_Rol)
        {
            try
            {
                List<DtoUsuarioMuestra> lista = await UServices.GetUsuariosPorRol(id_Rol);

                return Ok(lista);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);//No se encontró el recurso solicitado.
            }
        }

        [HttpPost("CrearAdminOAuditor/")]//Solo el admin
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> PostAdmin(DtoUsuario dtoUsuario, int IdRol)
        {
            try
            {
                if (dtoUsuario == null)
                {
                    return BadRequest("Datos de usuario mal formados"); //Los datos enviados están mal formados, faltan, o no cumplen una validación.
                }
                dtoUsuario.Id_Rol = IdRol; //IdRol = 1 Crear admin, IdRol = 3 Crear auditor: Segun el id de tu tabla roles
                await UServices.PostCrearAdmin(dtoUsuario);

                return Ok(new
                {
                    Mensaje = "el Usuario con el Email: " + dtoUsuario.Email + " fue creado correctamente."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return Conflict(ex.Message);//409 == Conflicto de datos, como intentar registrar un email ya existente
            }


        }
        [HttpPut]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> EditarUsuario(DtoUsuario dtoUsuario, string UserNameDelUsuarioAEditar)
        {
            try
            {
                if (dtoUsuario == null || UserNameDelUsuarioAEditar == null) return BadRequest("Complete los campos correspondientes");
                
                await UServices.EditarUsuario(dtoUsuario, UserNameDelUsuarioAEditar);

                return Ok(new
                {
                    Mensaje = "Usuario " + dtoUsuario.UserName + " Editado."
                });

            }catch(KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize(Policy = "ControlUsuarios")]
        public async Task<IActionResult> DeleteUsuario(string userName)
        {
            try
            {
                if (userName == null) return BadRequest("Error: Ingrese el username del usuario a eliminar");


                await UServices.EliminarUsuario(userName);

                return Ok(new
                {
                    Mensaje = "Usuario: " + userName + " Eliminado."
                });
            }catch(KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);  
            }
        }

    }
}
