using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examen.Data;
using examen.Models;

namespace examen.Controllers
{
    public class ReparacionsController : Controller
    {
        private readonly AppDbContext _context;

        public ReparacionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reparacions
        public async Task<IActionResult> Index()
        {
            // Incluye datos relacionados con equipos
            var reparaciones = _context.Reparaciones.Include(r => r.Equipo);
            return View(await reparaciones.ToListAsync());
        }

        // GET: Reparacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de la reparación no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var reparacion = await _context.Reparaciones
                .Include(r => r.Equipo)
                .FirstOrDefaultAsync(m => m.ReparacionID == id);

            if (reparacion == null)
            {
                TempData["ErrorMessage"] = "Reparación no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(reparacion);
        }

        // GET: Reparacions/Create
        public IActionResult Create()
        {
            ViewData["EquipoID"] = new SelectList(_context.Equipos, "EquipoID", "Modelo"); // Mostrar modelo del equipo
            return View();
        }

        // POST: Reparacions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReparacionID,FechaSolicitud,Estado,EquipoID")] Reparacion reparacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reparacion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Reparación creada exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Error al crear la reparación. Revisa los datos proporcionados.";
            ViewData["EquipoID"] = new SelectList(_context.Equipos, "EquipoID", "Modelo", reparacion.EquipoID);
            return View(reparacion);
        }

        // GET: Reparacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de la reparación no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var reparacion = await _context.Reparaciones.FindAsync(id);
            if (reparacion == null)
            {
                TempData["ErrorMessage"] = "Reparación no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipoID"] = new SelectList(_context.Equipos, "EquipoID", "Modelo", reparacion.EquipoID);
            return View(reparacion);
        }

        // POST: Reparacions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReparacionID,FechaSolicitud,Estado,EquipoID")] Reparacion reparacion)
        {
            if (id != reparacion.ReparacionID)
            {
                TempData["ErrorMessage"] = "ID de la reparación no coincide.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reparacion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Reparación editada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReparacionExists(reparacion.ReparacionID))
                    {
                        TempData["ErrorMessage"] = "Reparación no encontrada.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            TempData["ErrorMessage"] = "Error al editar la reparación. Revisa los datos proporcionados.";
            ViewData["EquipoID"] = new SelectList(_context.Equipos, "EquipoID", "Modelo", reparacion.EquipoID);
            return View(reparacion);
        }

        // GET: Reparacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de la reparación no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var reparacion = await _context.Reparaciones
                .Include(r => r.Equipo)
                .FirstOrDefaultAsync(m => m.ReparacionID == id);

            if (reparacion == null)
            {
                TempData["ErrorMessage"] = "Reparación no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(reparacion);
        }

        // POST: Reparacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reparacion = await _context.Reparaciones.FindAsync(id);
            if (reparacion != null)
            {
                _context.Reparaciones.Remove(reparacion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Reparación eliminada exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar la reparación. No se encontró en la base de datos.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReparacionExists(int id)
        {
            return _context.Reparaciones.Any(e => e.ReparacionID == id);
        }
    }
}
