using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Reserva
    {

        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yy}")]
        public DateTime Fecha { get; set; }

        public int PacienteId { get; set; }

        public Paciente Paciente { get; set; }

    }
}
