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
    public class LibrosAutoresController : Controller
    {
        private readonly Proyecto1Context _context;

        public LibrosAutoresController(Proyecto1Context context)
        {
            _context = context;
        }

        // GET: LibrosAutores
        public async Task<IActionResult> Index()
        {
            var proyecto1Context = _context.LibrosAutores.Include(l => l.IdAutorNavigation).Include(l => l.IsbnNavigation);
            return View(await proyecto1Context.ToListAsync());
        }

        // GET: LibrosAutores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutore = await _context.LibrosAutores
                .Include(l => l.IdAutorNavigation)
                .Include(l => l.IsbnNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (librosAutore == null)
            {
                return NotFound();
            }

            return View(librosAutore);
        }

        // GET: LibrosAutores/Create
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor");
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn");


            return View();
        }

        // POST: LibrosAutores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutor,Isbn")] LibrosAutore librosAutore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librosAutore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor", librosAutore.IdAutor);
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutore.Isbn);
            return View(librosAutore);
        }



        // GET: LibrosAutores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutore = await _context.LibrosAutores.FindAsync(id);
            if (librosAutore == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor", librosAutore.IdAutor);
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutore.Isbn);
            return View(librosAutore);
        }

        // POST: LibrosAutores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutor,Isbn")] LibrosAutore librosAutore)
        {
            if (id != librosAutore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librosAutore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrosAutoreExists(librosAutore.Id))
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
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "IdAutor", librosAutore.IdAutor);
            ViewData["Isbn"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutore.Isbn);
            return View(librosAutore);
        }

        // GET: LibrosAutores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutore = await _context.LibrosAutores
                .Include(l => l.IdAutorNavigation)
                .Include(l => l.IsbnNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (librosAutore == null)
            {
                return NotFound();
            }

            return View(librosAutore);
        }

        // POST: LibrosAutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var librosAutore = await _context.LibrosAutores.FindAsync(id);
                if (librosAutore == null)
                {
                    return Json(new { success = false, message = "El registro no fue encontrado." });
                }

                _context.LibrosAutores.Remove(librosAutore);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente." });
            }
            catch (Exception ex)
            {
                // Manejar errores inesperados
                return Json(new { success = false, message = "Ocurrió un error al intentar eliminar el registro: " + ex.Message });
            }
        }



        private bool LibrosAutoreExists(int id)
        {
            return _context.LibrosAutores.Any(e => e.Id == id);
        }
    }
}
