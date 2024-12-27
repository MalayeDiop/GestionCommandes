using System.ComponentModel.DataAnnotations.Schema;
using GestionCommandes.Enum;

namespace GestionCommandes.Models
{
    public class Commande : AbstractEntity
    {
        public DateTime DateCom { get; set;}
        public double Montant { get; set;}
        public int ClientId { get; set;}
        public Livreur livreur { get; set;}
        public int LivreurId { get; set;}
        [NotMapped]
        public Paiement Paiement { get; set;}
        [NotMapped]
        public int PaiementId { get; set;}
        [NotMapped]
        public List<DetailsCommande> DetailsCommande { get; set;}
        public EtatCommande EtatCom { get; set;}
    }
}