using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCommandes.Models
{
    public class Produit : AbstractEntity
    {
        public string Libelle { get; set;}
        public int PrixUnitaire { get; set;}
        public double QteStock { get; set;}
        [NotMapped]
        public List<DetailsCommande> DetailsCommande { get; set;}
    }
}