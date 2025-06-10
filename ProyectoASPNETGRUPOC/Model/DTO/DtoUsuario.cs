using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
    public class DtoUsuario
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [JsonIgnore]
        public int Id_Rol { get; set; } = 2;
    }
}
