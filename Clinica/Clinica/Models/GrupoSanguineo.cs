using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public enum GrupoSanguineo
    {
        [Display(Name = "A+")]
        Apositivo,
        [Display(Name = "A-")]
        Anegativo,
        [Display(Name = "B+")]
        Bpositivo,
        [Display(Name = "B-")]
        Bnegativo,
        [Display(Name = "AB+")]
        ABpositivo,
        [Display(Name = "AB-")]
        ABnegativo,
        [Display(Name = "0+")]
        CeroPositivo,
        [Display(Name = "0-")]
        CeroNegativo,
    }
}
