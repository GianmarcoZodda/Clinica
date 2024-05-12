using Clinica.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Turno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Errors._required)]
        public DateTime Fecha { get; set; }

        public int MedicoId { get; set; }

        public Medico Medico { get; set; }

        [Required(ErrorMessage = Errors._required)]
        public bool Disponible { get; set; } = true;

    }
}
