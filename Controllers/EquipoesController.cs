using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examen.Data;
using examen.Models;

namespace examen.Controllers
{
    public class EquipoesController : Controller
    {
        private readonly AppDbContext _context;

        public EquipoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Equipoes
        public async Task<IActionResult> Index()
        {
            var equipos = _context.Equipos.Include(e => e.Usuario);
            return View(await equipos.ToListAsync());
        }

        // GET: Equipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del equipo no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var equipo = await _context.Equipos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EquipoID == id);

            if (equipo == null)
            {
                TempData["ErrorMessage"] = "Equipo no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(equipo);
        }

        // GET: Equipoes/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre"); // Mostrar nombres de usuario
            return View();
        }

        // POST: Equipoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipoID,TipoEquipo,Modelo,UsuarioID")] Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error al crear el equipo. Revisa los datos.";
                ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre", equipo.UsuarioID);
                return View(equipo);
            }

            try
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Equipo creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al guardar el equipo: {ex.Message}";
                ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre", equipo.UsuarioID);
                return View(equipo);
            }
        }

        // GET: Equipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del equipo no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                TempData["ErrorMessage"] = "Equipo no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre", equipo.UsuarioID);
            return View(equipo);
        }

        // POST: Equipoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoID,TipoEquipo,Modelo,UsuarioID")] Equipo equipo)
        {
            if (id != equipo.EquipoID)
            {
                TempData["ErrorMessage"] = "ID del equipo no coincide.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error al editar el equipo. Revisa los datos.";
                ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre", equipo.UsuarioID);
                return View(equipo);
            }

            try
            {
                _context.Update(equipo);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Equipo editado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al actualizar el equipo: {ex.Message}";
                ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre", equipo.UsuarioID);
                return View(equipo);
            }
        }

        // GET: Equipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del equipo no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var equipo = await _context.Equipos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EquipoID == id);

            if (equipo == null)
            {
                TempData["ErrorMessage"] = "Equipo no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(equipo);
        }

        // POST: Equipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                TempData["ErrorMessage"] = "Error al eliminar el equipo. No se encontró en la base de datos.";
                return RedirectToAction(nameof(Index));
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Equipo eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.EquipoID == id);
        }
    }
}
