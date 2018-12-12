using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Models;

namespace SistemaAcademico.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly SistemaAcademicoContext _context;

        public EstudiantesController(SistemaAcademicoContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var sistemaAcademicoContext = _context.Estudiante.Include(e => e.Carrera);
            return View(await sistemaAcademicoContext.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .Include(e => e.Carrera)
                .SingleOrDefaultAsync(m => m.NoControl == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["carreraID"] = new SelectList(_context.Carrera, "carreraID", "carreraID");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoControl,Nombre,Apellidos,carreraID")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["carreraID"] = new SelectList(_context.Carrera, "carreraID", "carreraID", estudiante.carreraID);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante.SingleOrDefaultAsync(m => m.NoControl == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["carreraID"] = new SelectList(_context.Carrera, "carreraID", "carreraID", estudiante.carreraID);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoControl,Nombre,Apellidos,carreraID")] Estudiante estudiante)
        {
            if (id != estudiante.NoControl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.NoControl))
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
            ViewData["carreraID"] = new SelectList(_context.Carrera, "carreraID", "carreraID", estudiante.carreraID);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .Include(e => e.Carrera)
                .SingleOrDefaultAsync(m => m.NoControl == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiante.SingleOrDefaultAsync(m => m.NoControl == id);
            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.NoControl == id);
        }
    }
}
