using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoASPNETGRUPOC.Model.DTO
{
    public class DtoCuponesClientes
    {

        public string NroCupon { get; set; }
        public string NombreCupon { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAsignado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal? PorcentajeDto { get; set; }
        public decimal? ImportePromo { get; set; }
        public string TipoCupon { get; set; }
        public string Estado { get; set; }
    }
}
