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


        public int PacienteId { get; set; }

        public Paciente Paciente { get; set; }

        [Required(ErrorMessage = Errors._required)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = Errors._strlength)]
        public string Matricula { get; set; } = Generadores.GetNewMatricula(5);

        [StringLength(50, MinimumLength = 2, ErrorMessage = Errors._strlength)]
        [Display(Name = "Profesional")]
        public string ApellidoProfesional { get; set; }
    }
}
