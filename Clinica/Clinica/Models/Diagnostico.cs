using Clinica.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = Errors._strlength)]
        public string Descripcion { get; set; }


        public int PersonaId { get; set; }

        public Persona Persona { get; set; }

        [Required(ErrorMessage = Errors._required)]
        public DateTime Fecha { get; set; }
    }
}
