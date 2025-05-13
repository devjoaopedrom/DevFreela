using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;

        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<List<SkillViewModel>> GetAll(string search = "")
        {
            var skills = _context.Skills
                .ToList();

            var model = skills.Select(SkillViewModel.FromEntity).ToList();
            return ResultViewModel<List<SkillViewModel>>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
