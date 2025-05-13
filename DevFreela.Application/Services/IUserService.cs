using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using Microsoft.AspNetCore.Http;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {

        public ResultViewModel<UserViewModel> GetById(int id);
        public ResultViewModel<int> Insert(CreateUserInputModel model);
        public ResultViewModel InsertSkill(int id, UserSkillsInputModel model);
    }
}
