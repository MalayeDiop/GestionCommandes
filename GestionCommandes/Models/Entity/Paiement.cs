using GestionCommandes.Enum;

namespace GestionCommandes.Models
{
    public class Paiement : AbstractEntity
    {
        public TypePaiement TypePaiement { get; set;}
        public Commande Commande { get; set;}
        public int CommandeId { get; set;}
        public Boolean HasReduction { get; set;} = false;
    }
}