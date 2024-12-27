using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCommandes.Models
{
    public class Client : AbstractEntity
    {
        public double Solde { get; set;}
        public string Adresse { get; set;}
        [NotMapped]
        public List<Commande> Commandes { get; set;}
        public User User { get; set;}
        public int UserId { get; set;}
    }
}