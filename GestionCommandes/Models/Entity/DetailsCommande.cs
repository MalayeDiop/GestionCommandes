namespace GestionCommandes.Models
{
    public class DetailsCommande : AbstractEntity
    {
        public double QteCom { get; set;}
        public Commande Commande { get; set;}
        public int CommandeId { get; set;}
        public double PrixTotal { get; set;}
        public Produit Produit { get; set;}
        public int ProduitId { get; set;}
        
    }
}