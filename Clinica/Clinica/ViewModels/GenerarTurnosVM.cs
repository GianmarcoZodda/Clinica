using Clinica.Helpers;
using Clinica.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinica.ViewModels
{
    public class GenerarTurnosVM
    {
        public int MedicoId { get; set; }

        public Medico Medico { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = Errors._required)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = Errors._required)]
        public DateTime FechaFin { get; set; }
    }
}
