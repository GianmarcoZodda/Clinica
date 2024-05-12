using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

namespace Clinica.Controllers
{
    public class DiagnosticosController : Controller
    {
        private readonly ClinicaContext _context;

        public DiagnosticosController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Diagnosticos
        public async Task<IActionResult> Index(string? mensaje)
        {
            ViewBag.mensaje = mensaje;

            var clinicaContext = _context.Diagnosticos.Include(d => d.Paciente);
            return View(await clinicaContext.ToListAsync());
        }

        // GET: Diagnosticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticos/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido");
            return View();
        }

        // POST: Diagnosticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,PacienteId,Matricula")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                diagnostico.Fecha = DateTime.Now;
                try
                {
                    string nombreCompleto = this.BuscarProfesionalPorMatricula(diagnostico.Matricula);
                    diagnostico.ApellidoProfesional = nombreCompleto;
                }
                catch
                {
                    return RedirectToAction("Index", new { Mensaje = "ocurrio un error, asegurate de escribir tu matricula correctamente" });
                }
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NombreCompleto", diagnostico.PacienteId);
            return View(diagnostico);
        }


        private string BuscarProfesionalPorMatricula(string matricula)
        {
            string nombreCompleto = _context.Medicos.FirstOrDefault(m => m.Matricula == matricula).NombreCompleto;
            if (nombreCompleto == null)
            {
                return null;
            }
            return nombreCompleto;
        }

        // GET: Diagnosticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", diagnostico.PacienteId);
            return View(diagnostico);
        }

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,PacienteId,Fecha,Matricula,ApellidoProfesional")] Diagnostico diagnostico)
        {
            //if (id != diagnostico.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(diagnostico);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!DiagnosticoExists(diagnostico.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Apellido", diagnostico.PacienteId);
            //return View(diagnostico);

            return RedirectToAction("Index", "Home", new { mensaje = "una vez creado el diagnostico, este no puede editarse." });
        }

        // GET: Diagnosticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            //agregar una forma de validar que el unico que puede borrar el diagnostico es el medico que lo creo

            return View(diagnostico);
        }

        // POST: Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnosticos.Remove(diagnostico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
            return _context.Diagnosticos.Any(e => e.Id == id);
        }
    }
}
