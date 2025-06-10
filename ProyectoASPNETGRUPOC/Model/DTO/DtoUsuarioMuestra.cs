namespace ProyectoASPNETGRUPOC.Model.DTO
{
    public class DtoUsuarioMuestra
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Email { get; set; }

        public string? tipoUsuario { get; set; }
    }
}
