using Clinica.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Medico : Persona
    {
        [Required(ErrorMessage = Errors._required)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = Errors._maxmin)]
        public string Matricula { get; set; } = Generadores.GetNewMatricula(5);

        public int EspecialidadId { get; set; }

        public Especialidad Especialidad { get; set; }
    }
}
