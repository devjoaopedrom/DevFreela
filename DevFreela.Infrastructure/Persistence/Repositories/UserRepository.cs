using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;

        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserSkill>> AddSkill(List<UserSkill> userSkills)
        {
            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();
            return userSkills;
        }

        public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string passwordHashed)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == passwordHashed );
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.FullName)
                .Include(u => u.Email)
                .Include(u => u.Skills)
                .SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
