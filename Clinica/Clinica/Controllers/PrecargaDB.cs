using Clinica.Data;
using Clinica.Helpers;
using Clinica.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.Controllers
{
    public class PrecargaDB : Controller
    {
        private ClinicaContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;


        public PrecargaDB(ClinicaContext context, UserManager<Persona> userManager,
            RoleManager<Rol> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }



        public IActionResult Seed()
        {
            CrearRoles().Wait();

            if (!_context.Personas.Any())
            {
                this.AddAdmin().Wait();
            }

            if (!_context.Especialidades.Any())
            {
                this.AddEspecialidades();
            }

            if (!_context.Consultorios.Any())
            {
                this.AddConsultorios();
            }

            if (!_context.Medicos.Any())
            {
                this.AddMedicos().Wait();
            }

            if (!_context.Pacientes.Any())
            {
                this.AddPacientes().Wait();
            }



            return RedirectToAction("Index", "Home", new { mensaje = "Hice Precarga" });

        }


        public IActionResult Remove()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return RedirectToAction("Index", "Home", new { mensaje = "Borre" });
        }

        private async Task CrearRoles()
        {
            Rol rolcliente = new Rol() { Name = "MedicoRol" };
            Rol rolempleado = new Rol() { Name = "PacienteRol" };
            Rol roladmin = new Rol() { Name = "AdminRol" };

            await _roleManager.CreateAsync(rolcliente);
            await _roleManager.CreateAsync(rolempleado);
            await _roleManager.CreateAsync(roladmin);
        }


        private async Task AddAdmin()
        {
            Persona admin = new Persona()
            {
                Nombre = "Lionel",
                Apellido = "Messi",
                Direccion = "Belgrano",
                UserName = "lionel@messi.com.ar",
                Email = "lionel@messi.com.ar",
                FechaAlta = DateTime.Now,
            };
            await _userManager.CreateAsync(admin, "Password1!");
            await _userManager.AddToRoleAsync(admin, "AdminRol");
        }



        //       ESPECIALIDADES        //

        private void AddEspecialidades()
        {
            Especialidad esp1 = new Especialidad()
            {
                Nombre = "Dermatologia"
            };
            _context.Especialidades.Add(esp1);
            _context.SaveChanges();

            Especialidad esp2 = new Especialidad()
            {
                Nombre = "Traumatologia"
            };
            _context.Especialidades.Add(esp2);
            _context.SaveChanges();

            Especialidad esp3 = new Especialidad()
            {
                Nombre = "Cardiologia"
            };
            _context.Especialidades.Add(esp3);
            _context.SaveChanges();

            Especialidad esp4 = new Especialidad()
            {
                Nombre = "Psiquiatria"
            };
            _context.Especialidades.Add(esp4);
            _context.SaveChanges();

            Especialidad esp5 = new Especialidad()
            {
                Nombre = "Pediatria"
            };
            _context.Especialidades.Add(esp5);
            _context.SaveChanges();
        }


        private void AddConsultorios()
        {
            Consultorio cons1 = new Consultorio()
            {
                Nombre = "Deragopyan",
                Direccion = "jonas salk 1781",
                TelefonoEmergencias = 2222222,
                TelefonoConsultas = 3333333,
                Email = "consultorio1@prueba.com.ar",

            };
            _context.Consultorios.Add(cons1);
            _context.SaveChanges();

            Consultorio cons2 = new Consultorio()
            {
                Nombre = "Hospital Naval",
                Direccion = "frente al parque centenario",
                TelefonoEmergencias = 3333333,
                TelefonoConsultas = 4444444,
                Email = "consultorio12@prueba.com.ar",

            };
            _context.Consultorios.Add(cons2);
            _context.SaveChanges();

            Consultorio cons3 = new Consultorio()
            {
                Nombre = "La Merced",
                Direccion = "sargento plama 1438",
                TelefonoEmergencias = 5555555,
                TelefonoConsultas = 6666666,
                Email = "consultorio3@prueba.com.ar",

            };
            _context.Consultorios.Add(cons3);
            _context.SaveChanges();
        }

        private async Task AddMedicos()
        {
            Medico m1 = new Medico()
            {
                Matricula = Generadores.GetNewMatricula(7),
                Nombre = "Gianmarco",
                Apellido = "Zodda",
                Direccion = "Mitre 6160",
                Foto = "notiene.jpg",
                UserName = "Gianmarcus22",
                Password = "Password1!",
                Email = "medico1j@gmail.com.ar",
                FechaAlta = DateTime.Now,
                ConsultorioId = BuscarConsultorio("Deragopyan"),
                EspecialidadId = BuscarEspecialidad("Dermatologia"),
            };
            _context.Medicos.Add(m1);
            _context.SaveChanges();

            //await _userManager.CreateAsync(m1, "Password1!");
            //await _userManager.AddToRoleAsync(m1, "MedicoRol")

            Medico m2 = new Medico()
            {
                Matricula = Generadores.GetNewMatricula(7),
                Nombre = "Brisa",
                Apellido = "Toledo",
                Direccion = "Moreno",
                Foto = "notiene.jpg",
                UserName = "BrisaT",
                Password = "Password1!",
                Email = "medico2@gmail.com.ar",
                FechaAlta = DateTime.Now,
                ConsultorioId = BuscarConsultorio("Hospital Naval"),
                EspecialidadId = BuscarEspecialidad("Traumatologia"),
            };
            _context.Medicos.Add(m2);
            _context.SaveChanges();

            //await _userManager.CreateAsync(m2, "Password1!");
            //await _userManager.AddToRoleAsync(m2, "MedicoRol")

            Medico m3 = new Medico()
            {
                Matricula = Generadores.GetNewMatricula(7),
                Nombre = "Esteben",
                Apellido = "Morleo",
                Direccion = "Jonas Salk 1781",
                Foto = "notiene.jpg",
                UserName = "ElEsteben",
                Password = "Password1!",
                Email = "medico3@gmail.com",
                FechaAlta = DateTime.Now,
                ConsultorioId = BuscarConsultorio("Deragopyan"),
                EspecialidadId = BuscarEspecialidad("Dermatologia"),
            };
            _context.Medicos.Add(m3);
            _context.SaveChanges();

            //await _userManager.CreateAsync(m3, "Password1!");
            //await _userManager.AddToRoleAsync(m3, "MedicoRol")


            Medico m4 = new Medico()
            {
                Matricula = Generadores.GetNewMatricula(7),
                Nombre = "Gabriela",
                Apellido = "Soba",
                Direccion = "Parana",
                Foto = "notiene.jpg",
                UserName = "Mama",
                Password = "Password1!",
                Email = "medico4@gmail.com",
                FechaAlta = DateTime.Now,
                ConsultorioId = BuscarConsultorio("Hospital Naval"),
                EspecialidadId = BuscarEspecialidad("Cardiologia"),
            };
            _context.Medicos.Add(m4);
            _context.SaveChanges();

            //await _userManager.CreateAsync(m4, "Password1!");
            //await _userManager.AddToRoleAsync(m4, "MedicoRol")
        }

        private async Task AddPacientes()
        {
            Paciente p1 = new Paciente()
            {
                Nombre = "Matias",
                Apellido = "Rodriguez",
                Direccion = "club victoria",
                Foto = "notiene.jpg",
                UserName = "Matuz2002",
                Password = "Password1!",
                Email = "paciente1j@gmail.com.ar",
                FechaAlta = DateTime.Now,
                Donante = true,
                Alergias = "no tengo",
                Peso = 72,
                Altura = 173,
                DNI = 44211766,
                Edad = 21,
                GrupoSanguineo = GrupoSanguineo.Apositivo,
                ConsultorioId = BuscarConsultorio("Deragopyan"),
            };
            _context.Pacientes.Add(p1);
            _context.SaveChanges();

            //await _userManager.CreateAsync(p1, "Password1!");
            //await _userManager.AddToRoleAsync(p1, "PacienteRol")


            Paciente p2 = new Paciente()
            {
                Nombre = "Nicolas",
                Apellido = "Herrero",
                Direccion = "San Miguel",
                Foto = "notiene.jpg",
                UserName = "Nicox",
                Password = "Password1!",
                Email = "paciente2j@gmail.com.ar",
                FechaAlta = DateTime.Now,
                Donante = false,
                Alergias = "penicilina",
                Peso = 90,
                Altura = 189,
                DNI = 44211766,
                Edad = 26,
                GrupoSanguineo = GrupoSanguineo.Anegativo,
                ConsultorioId = BuscarConsultorio("Hospital Naval"),
            };
            _context.Pacientes.Add(p2);
            _context.SaveChanges();

            //await _userManager.CreateAsync(p2, "Password1!");
            //await _userManager.AddToRoleAsync(p2, "PacienteRol")


            Paciente p3 = new Paciente()
            {
                Nombre = "Kevin",
                Apellido = "Ghersi",
                Direccion = "William Morris",
                Foto = "notiene.jpg",
                UserName = "Geeerrsii",
                Password = "Password1!",
                Email = "paciente3j@gmail.com.ar",
                FechaAlta = DateTime.Now,
                Donante = true,
                Alergias = "ninguna",
                Peso = 80,
                Altura = 180,
                DNI = 44211766,
                Edad = 24,
                GrupoSanguineo = GrupoSanguineo.Bnegativo,
                ConsultorioId = BuscarConsultorio("Deragopyan"),
            };
            _context.Pacientes.Add(p3);
            _context.SaveChanges();

            //await _userManager.CreateAsync(p3, "Password1!");
            //await _userManager.AddToRoleAsync(p3, "PacienteRol")


            Paciente p4 = new Paciente()
            {
                Nombre = "Celeste",
                Apellido = "Toledo",
                Direccion = "Caseros",
                Foto = "notiene.jpg",
                UserName = "Celes",
                Password = "Password1!",
                Email = "paciente4j@gmail.com.ar",
                FechaAlta = DateTime.Now,
                Donante = false,
                Alergias = "abejas",
                Peso = 66,
                Altura = 164,
                DNI = 44211766,
                Edad = 23,
                GrupoSanguineo = GrupoSanguineo.Bpositivo,
                ConsultorioId = BuscarConsultorio("Hospital Naval"),
            };
            _context.Pacientes.Add(p4);
            _context.SaveChanges();

            //await _userManager.CreateAsync(p4, "Password1!");
            //await _userManager.AddToRoleAsync(p4, "PacienteRol")
        }

        private void AddTurnos()
        {

        }

        private void AddDiagnosticos()
        {

        }


        private int BuscarConsultorio(string nombre)
        {
            int ConsultorioId = 1;
            Consultorio Consultorio = _context.Consultorios.FirstOrDefault(c => c.Nombre == nombre);
            if (Consultorio != null)
            {
                ConsultorioId = Consultorio.Id;
            }
            return ConsultorioId;
        }

        private int BuscarEspecialidad(string nombre)
        {
            int EspecialidadId = 1;
            Especialidad Especialidad = _context.Especialidades.FirstOrDefault(e => e.Nombre == nombre);
            if (Especialidad != null)
            {
                EspecialidadId = Especialidad.Id;
            }
            return EspecialidadId;
        }


    }
}
