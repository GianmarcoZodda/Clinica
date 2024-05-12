using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Clinica.Helpers;

namespace Clinica.Models
{
    public class Persona /*: IdentityUser<int>*/
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = Errors._strlength)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = Errors._required)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = Errors._strlength)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = Errors._required)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = Errors._strlength)]
        public string Direccion { get; set; }

        public string Foto { get; set; } = "default.jpg";

        //[StringLength(50, MinimumLength = 4, ErrorMessage = Errors._strlength)]
        //[Display(Name = "Usuario")]
        //public override string UserName
        //{
        //    get { return base.UserName; }
        //    set
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            base.UserName = base.Email;
        //        }
        //        else
        //        {
        //            base.UserName = value;
        //        }
        //    }
        //}


        [StringLength(50, MinimumLength = 4, ErrorMessage = Errors._strlength)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }


        [Required(ErrorMessage = Errors._required)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = Errors._strlength)]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "Password1!";


        //[Required(ErrorMessage = Errors._required)]
        //[EmailAddress(ErrorMessage = Errors._campoinvalido)]
        //public override string Email
        //{
        //    get { return base.Email; }
        //    set { base.Email = value; }
        //}

        [Required(ErrorMessage = Errors._required)]
        [EmailAddress(ErrorMessage = Errors._campoinvalido)]
        public string Email {  get; set; }

        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yy}")]
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto
        {
            get
            {
                return $"{Apellido}, {Nombre}";
            }
        }
    }
}
