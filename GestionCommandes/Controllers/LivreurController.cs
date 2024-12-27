using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionCommandes.Models;

namespace GestionCommandes.Controllers
{
    public class LivreurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivreurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Livreur
        public async Task<IActionResult> Index()
        {
            var livreurs = await _context.livreur.ToListAsync();
            return View(livreurs);
        }

        // GET: Livreur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livreur = await _context.livreur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livreur == null)
            {
                return NotFound();
            }

            return View(livreur);
        }

        // GET: Livreur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livreur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("isAvailable,Nom,Prenom,Telephone,Id,CreateAt,UpdateAt")] Livreur livreur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livreur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livreur);
        }

        // GET: Livreur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livreur = await _context.livreur.FindAsync(id);
            if (livreur == null)
            {
                return NotFound();
            }
            return View(livreur);
        }

        // POST: Livreur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("isAvailable,Nom,Prenom,Telephone,Id,CreateAt,UpdateAt")] Livreur livreur)
        {
            if (id != livreur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livreur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreurExists(livreur.Id))
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
            return View(livreur);
        }

        // GET: Livreur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livreur = await _context.livreur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livreur == null)
            {
                return NotFound();
            }

            return View(livreur);
        }

        // POST: Livreur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livreur = await _context.livreur.FindAsync(id);
            if (livreur != null)
            {
                _context.livreur.Remove(livreur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreurExists(int id)
        {
            return _context.livreur.Any(e => e.Id == id);
        }
    }
}
