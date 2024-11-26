using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using examen.Data;
using examen.Models;

namespace examen.Controllers
{
    public class TecnicoesController : Controller
    {
        private readonly AppDbContext _context;

        public TecnicoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tecnicoes
        public async Task<IActionResult> Index()
        {
            // Carga la lista de técnicos
            var tecnicos = await _context.Tecnicos.ToListAsync();
            return View(tecnicos);
        }

        // GET: Tecnicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del técnico no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var tecnico = await _context.Tecnicos.FirstOrDefaultAsync(m => m.TecnicoID == id);
            if (tecnico == null)
            {
                TempData["ErrorMessage"] = "Técnico no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(tecnico);
        }

        // GET: Tecnicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnicoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TecnicoID,Nombre,Especialidad")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnico);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Técnico creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Error al crear el técnico. Revisa los datos proporcionados.";
            return View(tecnico);
        }

        // GET: Tecnicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del técnico no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null)
            {
                TempData["ErrorMessage"] = "Técnico no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(tecnico);
        }

        // POST: Tecnicoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TecnicoID,Nombre,Especialidad")] Tecnico tecnico)
        {
            if (id != tecnico.TecnicoID)
            {
                TempData["ErrorMessage"] = "ID del técnico no coincide.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnico);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Técnico editado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicoExists(tecnico.TecnicoID))
                    {
                        TempData["ErrorMessage"] = "Técnico no encontrado.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            TempData["ErrorMessage"] = "Error al editar el técnico. Revisa los datos proporcionados.";
            return View(tecnico);
        }

        // GET: Tecnicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID del técnico no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var tecnico = await _context.Tecnicos.FirstOrDefaultAsync(m => m.TecnicoID == id);
            if (tecnico == null)
            {
                TempData["ErrorMessage"] = "Técnico no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(tecnico);
        }

        // POST: Tecnicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico != null)
            {
                _context.Tecnicos.Remove(tecnico);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Técnico eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el técnico. No se encontró en la base de datos.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TecnicoExists(int id)
        {
            return _context.Tecnicos.Any(e => e.TecnicoID == id);
        }
    }
}
