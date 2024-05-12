using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Clinica.Helpers;

namespace Clinica.Controllers
{
    public class MedicosController : Controller
    {
        private readonly ClinicaContext _context;

        public MedicosController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            var clinicaContext = _context.Medicos
                .Include(m => m.Consultorio)
                .Include(m => m.Especialidad)
                .Include(p => p.Turnos);
            return View(await clinicaContext.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Consultorio)
                .Include(m => m.Especialidad)
                .Include(p => p.Turnos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre");
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialidadId,Id,Nombre,Apellido,Direccion,Foto,Password,Email,ConsultorioId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                medico.Matricula = Generadores.GetNewMatricula(5);
                medico.FechaAlta = DateTime.Now; 
                medico.UserName = medico.Email;
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre", medico.ConsultorioId);
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre", medico.EspecialidadId);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Nombre", medico.ConsultorioId);
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre", medico.EspecialidadId);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matricula,EspecialidadId,Id,Nombre,Apellido,Direccion,Foto,UserName,Password,Email,ConsultorioId")] Medico updatedMedico)
        {
            if (id != updatedMedico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalMedico = await _context.Medicos.FirstOrDefaultAsync(m => m.Id == id);
                    originalMedico.Nombre = updatedMedico.Nombre;
                    originalMedico.Apellido = updatedMedico.Apellido;
                    originalMedico.Direccion = updatedMedico.Direccion;
                    originalMedico.Foto = updatedMedico.Foto;
                    originalMedico.UserName = updatedMedico.UserName;
                    originalMedico.ConsultorioId = updatedMedico.ConsultorioId;
                    _context.Update(originalMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(updatedMedico.Id))
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
            ViewData["ConsultorioId"] = new SelectList(_context.Consultorios, "Id", "Direccion", updatedMedico.ConsultorioId);
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "Id", "Nombre", updatedMedico.EspecialidadId);
            return View(updatedMedico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Consultorio)
                .Include(m => m.Especialidad)
                .Include(p => p.Turnos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
