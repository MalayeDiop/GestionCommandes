using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCommandes.Models
{
    public class Livreur : Personne
    {
        public Boolean isAvailable { get; set; } = true;
        [NotMapped]
        public List<Commande> Commandes { get; set;}
    }
}