using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionCommandes.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);
    }


   
    public DbSet<GestionCommandes.Models.Client> client { get; set; } = default!;

    public DbSet<GestionCommandes.Models.User> users { get; set; } = default!;

    public DbSet<GestionCommandes.Models.Commande> commande { get; set; } = default!;

    public DbSet<GestionCommandes.Models.DetailsCommande> detailCommande { get; set; } = default!;

    public DbSet<GestionCommandes.Models.Paiement> paiement { get; set; } = default!;

    public DbSet<GestionCommandes.Models.Produit> produit { get; set; } = default!;

    public DbSet<GestionCommandes.Models.Livreur> livreur { get; set; } = default!;



}