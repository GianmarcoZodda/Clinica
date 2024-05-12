using Clinica.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Paciente : Persona
    {
        [Required(ErrorMessage = Errors._required)]
        public bool Donante { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = Errors._strlength)]
        public string Alergias { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [Range(1, int.MaxValue, ErrorMessage = Errors._cantinvalida)]
        public double Peso { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [Range(1, int.MaxValue, ErrorMessage = Errors._cantinvalida)]
        public double Altura { get; set; }


        [Required(ErrorMessage = Errors._required)]
        [Range(1000000, 99999999, ErrorMessage = Errors._maxmin)]
        public int DNI { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [Range(1, int.MaxValue, ErrorMessage = Errors._cantinvalida)]
        public int Edad { get; set; }

        [Required(ErrorMessage = Errors._required)]
        public ObraSocial ObraSocial { get; set; }

        public GrupoSanguineo GrupoSanguineo { get; set; }


    }
}
