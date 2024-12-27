using Microsoft.EntityFrameworkCore;
using GestionCommandes.Enum;
using GestionCommandes.Models;

namespace GestionCommandes.Data.Fixtures
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            if (context.users.Any() || context.client.Any() || context.commande.Any() || context.livreur.Any() || context.produit.Any())
            {
                return;
            }
            for (int i = 1; i <= 5; i++)
            {
                var user = new User
                {
                    Nom = $"Nom{i}",
                    Prenom = $"Prenom{i}",
                    Telephone = $"77 123 45 6{i}",
                    Login = $"user{i}@",
                    Password = $"password{i}",
                    Role = (i % 3 == 0) ? Role.COMPTABLE : (i % 2 == 0) ? Role.CLIENT : Role.RS,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                if (i % 2 == 0)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        var client = new Client
                        {
                            Adresse = $"Adresse{j}",
                            Solde = i * 1000.0,
                            User = user,
                            UserId = i,
                            CreateAt = DateTime.UtcNow,
                            UpdateAt = DateTime.UtcNow,

                        };
                        context.client.Add(client);
                    }
                }
                context.users.Add(user);
            }

            for (int i = 1; i <= 5; i++)
            {
                var produits = new Produit
                {
                    Libelle = $"Produit{i}",
                    PrixUnitaire = i * 1000,
                    QteStock = i * 5,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                context.produit.Add(produits);
            }

            var livreurs = new List<Livreur>();
            for (int i = 1; i <= 5; i++)
            {
                var livreur = new Livreur
                {
                    Nom = $"LivNom{i}",
                    Prenom = $"LivPrenom{i}",
                    Telephone = $"77 123 45 1{i}",
                    isAvailable = (i % 2 == 0),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                livreurs.Add(livreur);
                context.livreur.Add(livreur);
            }

            context.SaveChanges();

            foreach (var livreur in livreurs)
            {
                for (int i = 1; i <= 5; i++)
                {
                    var commandes = new Commande
                    {
                        DateCom = DateTime.UtcNow,
                        Montant = 1000.0 * i,
                        ClientId = 1,
                        LivreurId = livreur.Id,
                        EtatCom = EtatCommande.ATTENTE,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };
                    context.commande.Add(commandes);
                    for (int j = 1; j <= 3; j++)
                    {
                        var detailsCommande = new DetailsCommande
                        {
                            QteCom = j * 2,
                            Commande = commandes,
                            CommandeId = i,
                            PrixTotal = j * 100.0,
                            ProduitId = j,
                            CreateAt = DateTime.UtcNow,
                            UpdateAt = DateTime.UtcNow
                        };
                        context.detailCommande.Add(detailsCommande);
                    }

                }
            }

            context.SaveChanges();

            for (int i = 1; i <= 5; i++)
            {
                var paiements = new Paiement
                {
                    TypePaiement = (i % 2 == 0) ? TypePaiement.WAVE : TypePaiement.OM,
                    CommandeId = i,
                    HasReduction = (i % 3 == 0),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                context.paiement.Add(paiements);
            }

            
            
            
            
            
            
            context.SaveChanges();
        }
    }
}