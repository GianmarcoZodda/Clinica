using Clinica.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Data
{
    public class ClinicaContext : DbContext
    {

        public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) { }

        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}
