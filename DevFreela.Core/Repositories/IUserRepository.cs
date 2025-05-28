using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetById(int id);
        Task<int> AddUser(User user);
        Task<List<UserSkill>> AddSkill(List<UserSkill> userSkills);
        Task<bool> Exists(int id);
    }
}
