using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class UserRepository(AuthContext dbContext) : IUserRepository
    {
        /// <inheritdoc/>
        public async Task<User> AddAsync(User entity)
        {
            dbContext.Users.Add(entity);
            _ = await dbContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(User entity)
        {
            dbContext.Users.Remove(entity);
            _ = await dbContext.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<User> UpdateAsync(User entity)
        {
            dbContext.Users.Update(entity);
            _ = await dbContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetAll()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<User?> GetById(Guid id)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User?> GetByEmail(string email)
        {
            return dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
