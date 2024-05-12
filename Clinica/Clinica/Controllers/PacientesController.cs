using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Clinica.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ClinicaContext _context;

        public PacientesController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var clinicaContext = _context.Pacientes
                .Include(p => p.Consultorio)
                .Include(p => p.Turnos)
                .Include(p => p.Diagnosticos);
            return View(await clinicaContext.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Consultorio)
                .Include(p => p.Turnos)
                .Include(p => p.Diagnosticos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre");

            var obraSocialValues = Enum.GetValues(typeof(ObraSocial)).Cast<ObraSocial>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            var grupoSanguineoValues = Enum.GetValues(typeof(GrupoSanguineo)).Cast<GrupoSanguineo>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewData["ObraSocial"] = new SelectList(obraSocialValues, "Value", "Text");
            ViewData["GrupoSanguineo"] = new SelectList(grupoSanguineoValues, "Value", "Text");


            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Donante,Alergias,Peso,Altura,DNI,Edad,ObraSocial,GrupoSanguineo,Id,Nombre,Apellido,Direccion,Foto,UserName,Password,Email,ConsultorioId")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.FechaAlta = DateTime.Now;
                if(paciente.UserName == null)
                {
                    paciente.UserName = paciente.Email;
                }
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre", paciente.ConsultorioId);

            var obraSocialValues = Enum.GetValues(typeof(ObraSocial)).Cast<ObraSocial>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            var grupoSanguineoValues = Enum.GetValues(typeof(GrupoSanguineo)).Cast<GrupoSanguineo>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewData["ObraSocial"] = new SelectList(obraSocialValues, "Value", "Text", paciente.ObraSocial);
            ViewData["GrupoSanguineo"] = new SelectList(grupoSanguineoValues, "Value", "Text", paciente.GrupoSanguineo);


            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre", paciente.ConsultorioId);

            var obraSocialValues = Enum.GetValues(typeof(ObraSocial)).Cast<ObraSocial>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            var grupoSanguineoValues = Enum.GetValues(typeof(GrupoSanguineo)).Cast<GrupoSanguineo>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewData["ObraSocial"] = new SelectList(obraSocialValues, "Value", "Text", paciente.ObraSocial);
            ViewData["GrupoSanguineo"] = new SelectList(grupoSanguineoValues, "Value", "Text", paciente.GrupoSanguineo);

            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Donante,Alergias,Peso,Altura,DNI,Edad,ObraSocial,GrupoSanguineo,Id,Nombre,Apellido,Direccion,Foto,UserName,Password,ConsultorioId,Email")] Paciente updatedPaciente)
        {
            if (id != updatedPaciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalPaciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
                    originalPaciente.Nombre = updatedPaciente.Nombre;
                    originalPaciente.Apellido = updatedPaciente.Apellido;
                    originalPaciente.Direccion = updatedPaciente.Direccion;
                    originalPaciente.Foto = updatedPaciente.Foto;
                    originalPaciente.UserName = updatedPaciente.UserName;
                    originalPaciente.ConsultorioId = updatedPaciente.ConsultorioId;
                    originalPaciente.ObraSocial = updatedPaciente.ObraSocial;
                    originalPaciente.GrupoSanguineo = updatedPaciente.GrupoSanguineo;
                    originalPaciente.Alergias = updatedPaciente.Alergias;
                    originalPaciente.Donante = updatedPaciente.Donante;
                    originalPaciente.Peso = updatedPaciente.Peso;
                    originalPaciente.Altura = updatedPaciente.Altura;
                    _context.Update(originalPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(updatedPaciente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre", updatedPaciente.ConsultorioId);

            var obraSocialValues = Enum.GetValues(typeof(ObraSocial)).Cast<ObraSocial>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            var grupoSanguineoValues = Enum.GetValues(typeof(GrupoSanguineo)).Cast<GrupoSanguineo>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewData["ObraSocial"] = new SelectList(obraSocialValues, "Value", "Text", updatedPaciente.ObraSocial);
            ViewData["GrupoSanguineo"] = new SelectList(grupoSanguineoValues, "Value", "Text", updatedPaciente.GrupoSanguineo);

            return View(updatedPaciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Consultorio)
                .Include(p => p.Turnos)
                .Include(p => p.Diagnosticos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
