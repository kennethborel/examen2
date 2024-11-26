using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examen.Data;
using examen.Models;

namespace examen.Controllers
{
    public class DetalleReparacionsController : Controller
    {
        private readonly AppDbContext _context;

        public DetalleReparacionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DetalleReparacions
        public async Task<IActionResult> Index()
        {
            // Incluye datos relacionados con reparaciones
            var detallesReparacion = _context.DetallesReparacion.Include(d => d.Reparacion);
            return View(await detallesReparacion.ToListAsync());
        }

        // GET: DetalleReparacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del detalle de reparación no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var detalleReparacion = await _context.DetallesReparacion
                .Include(d => d.Reparacion)
                .FirstOrDefaultAsync(m => m.DetalleID == id);

            if (detalleReparacion == null)
            {
                TempData["ErrorMessage"] = "Detalle de reparación no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(detalleReparacion);
        }

        // GET: DetalleReparacions/Create
        public IActionResult Create()
        {
            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion"); // Muestra descripción de la reparación
            return View();
        }

        // POST: DetalleReparacions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleID,Descripcion,FechaInicio,FechaFin,ReparacionID")] DetalleReparacion detalleReparacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleReparacion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Detalle de reparación creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Error al crear el detalle de reparación. Revisa los datos proporcionados.";
            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion", detalleReparacion.ReparacionID);
            return View(detalleReparacion);
        }

        // GET: DetalleReparacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del detalle de reparación no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var detalleReparacion = await _context.DetallesReparacion.FindAsync(id);
            if (detalleReparacion == null)
            {
                TempData["ErrorMessage"] = "Detalle de reparación no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion", detalleReparacion.ReparacionID);
            return View(detalleReparacion);
        }

        // POST: DetalleReparacions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleID,Descripcion,FechaInicio,FechaFin,ReparacionID")] DetalleReparacion detalleReparacion)
        {
            if (id != detalleReparacion.DetalleID)
            {
                TempData["ErrorMessage"] = "ID del detalle de reparación no coincide.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleReparacion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Detalle de reparación editado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleReparacionExists(detalleReparacion.DetalleID))
                    {
                        TempData["ErrorMessage"] = "Detalle de reparación no encontrado.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            TempData["ErrorMessage"] = "Error al editar el detalle de reparación. Revisa los datos proporcionados.";
            ViewData["ReparacionID"] = new SelectList(_context.Reparaciones, "ReparacionID", "Descripcion", detalleReparacion.ReparacionID);
            return View(detalleReparacion);
        }

        // GET: DetalleReparacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del detalle de reparación no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var detalleReparacion = await _context.DetallesReparacion
                .Include(d => d.Reparacion)
                .FirstOrDefaultAsync(m => m.DetalleID == id);

            if (detalleReparacion == null)
            {
                TempData["ErrorMessage"] = "Detalle de reparación no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(detalleReparacion);
        }

        // POST: DetalleReparacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleReparacion = await _context.DetallesReparacion.FindAsync(id);
            if (detalleReparacion != null)
            {
                _context.DetallesReparacion.Remove(detalleReparacion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Detalle de reparación eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el detalle de reparación. No se encontró en la base de datos.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DetalleReparacionExists(int id)
        {
            return _context.DetallesReparacion.Any(e => e.DetalleID == id);
        }
    }
}
