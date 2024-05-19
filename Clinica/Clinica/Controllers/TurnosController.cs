using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Clinica.ViewModels;

namespace Clinica.Controllers
{
    public class TurnosController : Controller
    {
        private readonly ClinicaContext _context;

        public TurnosController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public async Task<IActionResult> Index()
        {
            var clinicaContext = _context.Turnos.Include(t => t.Medico);
            return View(await clinicaContext.ToListAsync());
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,MedicoId,Disponible")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", turno.MedicoId);
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", turno.MedicoId);
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,MedicoId,Disponible")] Turno turno)
        {
            if (id != turno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.Id))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", turno.MedicoId);
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }



        [HttpGet]
        public IActionResult GenerarTurnos()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "NombreCompleto");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerarTurnos([Bind("MedicoId,FechaInicio,FechaFin")] GenerarTurnosVM generarTurnosVM)
        {
            if (ModelState.IsValid)
            {
                var medico = _context.Medicos.Find(generarTurnosVM.MedicoId);
                if (medico != null)
                {
                    var turnos = GenerarTurnosParaMedico(medico, generarTurnosVM.FechaInicio, generarTurnosVM.FechaFin);
                    _context.Turnos.AddRange(turnos);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home", new { mensaje = "se han generado los turnos"}); // Redirigir a la página principal
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El médico especificado no existe.");
                }
            }
            // Si llegamos aquí, hay un error, volvemos a mostrar el formulario con el ViewModel
            return View(generarTurnosVM);
        }

        private List<Turno> GenerarTurnosParaMedico(Medico medico, DateTime fechaInicio, DateTime fechaFin)
        {
            var turnos = new List<Turno>();
            for (DateTime fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
            {
                if (fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    for (int hora = 8; hora <= 14; hora++)
                    {
                        for (int minuto = 0; minuto < 60; minuto += 30)
                        {
                            turnos.Add(new Turno
                            {
                                Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, minuto, 0),
                                MedicoId = medico.Id,
                                Disponible = true
                            });
                        }
                    }
                }
            }
            return turnos;
        }




    }
}
