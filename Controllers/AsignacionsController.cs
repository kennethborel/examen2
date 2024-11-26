using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examen.Data;
using examen.Models;

namespace examen.Controllers
{
    public class AsignacionsController : Controller
    {
        private readonly AppDbContext _context;

        public AsignacionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Asignacions
        public async Task<IActionResult> Index()
        {
            var asignaciones = _context.Asignaciones
                .Include(a => a.Reparacion)
                .Include(a => a.Tecnico);
            return View(await asignaciones.ToListAsync());
        }

        // GET: Asignacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var asignacion = await _context.Asignaciones
                .Include(a => a.Reparacion)
                .Include(a => a.Tecnico)
                .FirstOrDefaultAsync(a => a.AsignacionID == id);

            if (asignacion == null)
                return NotFound();

            return View(asignacion);
        }

        // GET: Asignacions/Create
        public IActionResult Create()
        {
            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion");
            ViewData["TecnicoID"] = new SelectList(_context.Tecnicos, "TecnicoID", "Nombre");
            return View();
        }

        // POST: Asignacions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsignacionID,FechaAsignacion,ReparacionID,TecnicoID")] Asignacion asignacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion", asignacion.ReparacionID);
            ViewData["TecnicoID"] = new SelectList(_context.Tecnicos, "TecnicoID", "Nombre", asignacion.TecnicoID);
            return View(asignacion);
        }

        // GET: Asignacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion == null)
                return NotFound();

            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion", asignacion.ReparacionID);
            ViewData["TecnicoID"] = new SelectList(_context.Tecnicos, "TecnicoID", "Nombre", asignacion.TecnicoID);
            return View(asignacion);
        }

        // POST: Asignacions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsignacionID,FechaAsignacion,ReparacionID,TecnicoID")] Asignacion asignacion)
        {
            if (id != asignacion.AsignacionID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Asignaciones.Any(e => e.AsignacionID == id))
                        return NotFound();

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion", asignacion.ReparacionID);
            ViewData["TecnicoID"] = new SelectList(_context.Tecnicos, "TecnicoID", "Nombre", asignacion.TecnicoID);
            return View(asignacion);
        }

        // GET: Asignacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var asignacion = await _context.Asignaciones
                .Include(a => a.Reparacion)
                .Include(a => a.Tecnico)
                .FirstOrDefaultAsync(a => a.AsignacionID == id);

            if (asignacion == null)
                return NotFound();

            return View(asignacion);
        }

        // POST: Asignacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion != null)
            {
                _context.Asignaciones.Remove(asignacion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
