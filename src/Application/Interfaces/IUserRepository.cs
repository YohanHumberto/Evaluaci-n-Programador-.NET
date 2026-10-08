using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User entity);
        Task<bool> RemoveAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<List<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(string email);
    }
}
