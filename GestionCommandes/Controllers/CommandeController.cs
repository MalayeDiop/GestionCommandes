using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionCommandes.Models;
using GestionCommandes.Enum;

namespace GestionCommandes.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Commande
        public async Task<IActionResult> Index()
        {
            var commandes = await _context.commande
                                //   .Include(c => c.Client)
                                //   .Include(c => c.Livreur)
                                  .ToListAsync();

            return View(commandes);
        }

        // GET: Commande/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.commande
                .Include(c => c.livreur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        public async Task<IActionResult> CreateCommande(Dictionary<int, int> quantites)
        {
            if (quantites == null || !quantites.Any())
            {
                return BadRequest("Aucune quantité sélectionnée.");
            }

            var commande = new Commande
            {
                // DateCom = DateTime.Now,
                Montant = 0,
                EtatCom = EtatCommande.ATTENTE,
                ClientId = 1,
                LivreurId = 2,
            };

            foreach (var item in quantites)
            {
                var produit = await _context.produit.FindAsync(item.Key);
                if (produit != null)
                {
                    var quantiteCommandee = item.Value;
                    commande.Montant += produit.PrixUnitaire * quantiteCommandee;

                    var detailCommande = new DetailsCommande
                    {
                        ProduitId = produit.Id,
                        QteCom = quantiteCommandee,
                        Commande = commande
                    };

                    _context.detailCommande.Add(detailCommande);
                }
            }

            _context.Add(commande);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = commande.Id });
        }

        // GET: Commande/Payer/5
        public async Task<IActionResult> Payer(int? id)
        {
            if (id == null)
                return NotFound();

            var commande = await _context.commande
                // .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (commande == null)
                return NotFound();

            return View(commande);
        }

        // POST: Commande/Payer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayerConfirmed(int id, double montant, string servicePaiement)
        {
            if (montant <= 0)
            {
                ModelState.AddModelError("", "Le montant doit être supérieur à 0.");
                return RedirectToAction(nameof(Payer), new { id });
            }

            var commande = await _context.commande.FindAsync(id);

            if (commande == null)
                return NotFound();

            commande.Montant += montant;
            commande.DateCom = DateTime.UtcNow;

            if (commande.Montant >= commande.Montant)
            {
                commande.EtatCom = EtatCommande.PAYE;
                commande.Montant = commande.Montant;
            }
            else
            {
                commande.EtatCom = EtatCommande.IMPAYE;
            }

            _context.Update(commande);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Paiement de {montant}€ effectué avec succès via {servicePaiement}.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Commande/Create
        public IActionResult Create()
        {
            // Liste des livreurs
            ViewData["LivreurId"] = new SelectList(_context.livreur, "Id", "Nom");

            // Liste des clients
            ViewData["ClientId"] = new SelectList(_context.client, "Id", "Nom");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateCom,Montant,ClientId,LivreurId,EtatCom")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                var client = await _context.client.FindAsync(commande.ClientId);
                if (client == null)
                {
                    ModelState.AddModelError("", "Client non trouvé.");
                    return View(commande);
                }

                _context.Add(commande);
                await _context.SaveChangesAsync();

                // Rediriger vers la liste des commandes du client
                return RedirectToAction(nameof(ClientCommandes), new { clientId = commande.ClientId });
            }
            ViewData["LivreurId"] = new SelectList(_context.livreur, "Id", "Nom", commande.LivreurId);
            ViewData["ClientId"] = new SelectList(_context.client, "Id", "Nom", commande.ClientId);

            return View(commande);
        }

        public async Task<IActionResult> ClientCommandes(int clientId)
        {
            var commandes = await _context.commande
                                        .Where(c => c.ClientId == clientId)
                                        .Include(c => c.livreur)
                                        .ToListAsync();

            return View(commandes);
        }


        // GET: Commande/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.commande.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }
            ViewData["LivreurId"] = new SelectList(_context.livreur, "Id", "Id", commande.LivreurId);
            return View(commande);
        }

        // POST: Commande/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DateCom,Montant,ClientId,LivreurId,EtatCom,Id,CreateAt,UpdateAt")] Commande commande)
        {
            if (id != commande.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(commande.Id))
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
            ViewData["LivreurId"] = new SelectList(_context.livreur, "Id", "Id", commande.LivreurId);
            return View(commande);
        }

        // GET: Commande/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.commande
                .Include(c => c.livreur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Commande/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commande = await _context.commande.FindAsync(id);
            if (commande != null)
            {
                _context.commande.Remove(commande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeExists(int id)
        {
            return _context.commande.Any(e => e.Id == id);
        }
    }
}
