using Clinica.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Consultorio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = Errors._strlength)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = Errors._strlength)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [Range(1000000, 999999999999999, ErrorMessage = Errors._cantinvalida)]
        [Display(Name = "Telefono de Emergencias")]
        public long TelefonoEmergencias { get; set; }

        [Range(1000000, 999999999999999, ErrorMessage = Errors._cantinvalida)]
        [Display(Name = "Telefono de Consultas")]
        public long? TelefonoConsultas { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [EmailAddress(ErrorMessage = Errors._campoinvalido)]
        public string Email { get; set; }

    }
}
