using GestionCommandes.Enum;

namespace GestionCommandes.Models
{
    public class User : Personne
    {
        public string Login { get; set;}
        public string Password { get; set;}
        public Role Role { get; set;}
    }
}