using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Skills)
                .SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error("O Usuario não existe.");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(user.Id);
        }

        public ResultViewModel InsertSkill(int id, UserSkillsInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}