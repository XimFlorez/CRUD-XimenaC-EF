using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDXimenaCastanedaEF.Models;

namespace CRUDXimenaCastanedaEF.Controllers
{
    public class EditorialesController : Controller
    {
        private readonly Proyecto1Context _context;

        public EditorialesController(Proyecto1Context context)
        {
            _context = context;
        }

        // GET: Editoriales
        public async Task<IActionResult> Index(string buscar)
        {
            var editoriales = from Editoriales in _context.Editoriales select Editoriales;

            if (!String.IsNullOrEmpty(buscar))
            {
                editoriales = editoriales.Where(s => s.Nombre!.Contains(buscar));
            }
            return View(await editoriales.ToListAsync());
        }
            // GET: Editoriales/Details/5
            public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Nit == id);
            if (editoriale == null)
            {
                return NotFound();
            }

            return View(editoriale);
        }

        // GET: Editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nit,Nombre,Telefono,Direccion,Email,Sitioweb")] Editoriale editoriale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editoriale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editoriale);
        }

        // GET: Editoriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales.FindAsync(id);
            if (editoriale == null)
            {
                return NotFound();
            }
            return View(editoriale);
        }

        // POST: Editoriales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nit,Nombre,Telefono,Direccion,Email,Sitioweb")] Editoriale editoriale)
        {
            if (id != editoriale.Nit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editoriale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialeExists(editoriale.Nit))
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
            return View(editoriale);
        }

        // GET: Editoriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Nit == id);
            if (editoriale == null)
            {
                return NotFound();
            }

            return View(editoriale);
        }

        // POST: Editoriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Verificar si la editorial está siendo utilizada en la tabla 'Libros' mediante la clave foránea 'Nit_editorial'
            var registroEnUso = await _context.Libros
                .AnyAsync(r => r.NitEditorial == id); // Ahora se verifica el campo 'NitEditorial' en lugar de 'Nit'

            if (registroEnUso)
            {
                // Si la editorial está en uso, devolver un mensaje de error en formato JSON
                return Json(new { success = false, error = "No se puede eliminar porque está en uso en otra tabla." });
            }

            // Si la editorial no está en uso, proceder a eliminarla
            var editoriale = await _context.Editoriales.FindAsync(id);
            if (editoriale != null)
            {
                _context.Editoriales.Remove(editoriale);
                await _context.SaveChangesAsync();
                return Json(new { success = true }); // Devolver respuesta exitosa
            }

            return Json(new { success = false, error = "No se encontró la editorial." }); // Devolver error si no se encontró la editorial
        }


        private bool EditorialeExists(int id)
        {
            return _context.Editoriales.Any(e => e.Nit == id);
        }
    }
}
