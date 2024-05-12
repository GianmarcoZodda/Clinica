using Clinica.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Especialidad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = Errors._strlength)]
        public string Nombre { get; set; }

        public List<Medico> Medicos { get; set; }
    }
}
