using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Rol: IdentityRole<int>
    {
        //public int Id { get; set; }

        public Rol() : base()
        {

        }

        public Rol(string name) : base(name)
        {

        }

        [Display(Name = ("Nombre del rol"))]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
    }
}
