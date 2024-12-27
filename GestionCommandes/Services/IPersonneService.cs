using GestionCommandes.Models;

namespace GestionCommandes.Service
{
    public interface IPersonneService
    {
        Task<IEnumerable<Personne>> GetClienAsync();
        Task<Personne> Create(Personne personne);
        Task<Personne> Update(Personne personne);
    }
}