using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDXimenaCastanedaEF.Models;

namespace CRUDXimenaCastanedaEF.Controllers
{
    public class AutoresController : Controller
    {
        private readonly Proyecto1Context _context;

        public AutoresController(Proyecto1Context context)
        {
            _context = context;
        }

        // Buscar Datos (Evento Index)
        public async Task<IActionResult> Index(string buscar, string filtrar)
        {
            var autores = from Autores in _context.Autores select Autores;

            if (!string.IsNullOrEmpty(buscar))
            {
                autores = autores.Where(s => s.Nombre!.Contains(buscar));
            }
            ViewData["FiltroNombre"] = String.IsNullOrEmpty(filtrar) ? "NombreDescendente" : "";
            switch (filtrar)
            {
                case "NombreDescendente":
                    autores = autores.OrderByDescending(autor => autor.Nombre);
                    break;
            }

            return View(await autores.ToListAsync());
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autore == null)
            {
                return NotFound();
            }

            return View(autore);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutor,Nombre,Apellido,Telefono,Direccion,Email")] Autore autore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autore);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores.FindAsync(id);
            if (autore == null)
            {
                return NotFound();
            }
            return View(autore);
        }

        // POST: Autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutor,Nombre,Apellido,Telefono,Direccion,Email")] Autore autore)
        {
            if (id != autore.IdAutor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoreExists(autore.IdAutor))
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
            return View(autore);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autore == null)
            {
                return NotFound();
            }

            return View(autore);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Verificar si el autor está relacionado en la tabla 'LibrosAutores'
                var autorEnUso = await _context.LibrosAutores
                    .AnyAsync(la => la.IdAutor == id);

                if (autorEnUso)
                {
                 // Si el autor está en uso, devolver un mensaje 
                    return Json(new { success = false, message = "No se puede eliminar el autor porque está en uso en la tabla LibrosAutores." });
                }

                // Buscar el autor en la tabla Autores
                var autore = await _context.Autores.FindAsync(id);
                if (autore == null)
                {
                    // Si no se encuentra el autor, devolver un mensaje de error
                    return Json(new { success = false, message = "El autor no existe o ya fue eliminado." });
                }

                // Eliminar el autor si no tiene dependencias
                _context.Autores.Remove(autore);
                await _context.SaveChangesAsync();

                // Devolver una respuesta exitosa si la eliminación fue exitosa
                return Json(new { success = true, message = "Autor eliminado correctamente." });
            }
            catch (Exception ex)
            {
                // Devolver un mensaje detallado del error
                return Json(new { success = false, message = "Ocurrió un error al intentar eliminar el autor.", error = ex.Message });
            }
        }


        private bool AutoreExists(int id)
        {
            return _context.Autores.Any(e => e.IdAutor == id);
        }
    }
}
