using GestionCommandes.Models;

namespace GestionCommandes.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetClienAsync();
        Task<User> Create(User user);
        Task<User> Update(User user);
    }
}