using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using examen.Data;
using examen.Models;

namespace examen.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            // Ensure we handle potential null for the DbSet
            if (_context.Usuarios == null)
            {
                TempData["ErrorMessage"] = "No se pudo acceder a los usuarios.";
                return View(new List<Usuario>()); // Return an empty list if DbSet is null
            }

            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de usuario no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            if (_context.Usuarios == null)
            {
                TempData["ErrorMessage"] = "No se pudo acceder a los usuarios.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioID == id);

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioID,Nombre,CorreoElectronico,Telefono")] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error al crear el usuario. Revisa los datos ingresados.";
                return View(usuario);
            }

            if (_context.Usuarios != null)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Usuario creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "No se pudo guardar el usuario.";
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de usuario no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            if (_context.Usuarios == null)
            {
                TempData["ErrorMessage"] = "No se pudo acceder a los usuarios.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioID,Nombre,CorreoElectronico,Telefono")] Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                TempData["ErrorMessage"] = "El ID proporcionado no coincide.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error al editar el usuario. Revisa los datos ingresados.";
                return View(usuario);
            }

            try
            {
                if (_context.Usuarios != null)
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Usuario editado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "No se pudo guardar el usuario.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.UsuarioID == id))
                {
                    TempData["ErrorMessage"] = "Usuario no encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                throw;
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de usuario no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            if (_context.Usuarios == null)
            {
                TempData["ErrorMessage"] = "No se pudo acceder a los usuarios.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioID == id);

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                TempData["ErrorMessage"] = "No se pudo acceder a los usuarios.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios?.Any(e => e.UsuarioID == id) ?? false;
        }
    }
}
