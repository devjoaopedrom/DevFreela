using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DevFreela.Application.Commands.InsertSkillUser
{
    public class InsertSkillUserHandler : IRequestHandler<InsertSkillUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        public InsertSkillUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(InsertSkillUserCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds.Select(skillId => new UserSkill(request.UserId, skillId)).ToList();

            await _repository.AddSkill(userSkills);

            return ResultViewModel.Success();
        }
    }
}
