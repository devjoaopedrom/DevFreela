using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkillUser
{
    public class InsertSkillUserCommand : IRequest<ResultViewModel>
    {
        public int UserId { get; }
        public List<int> SkillIds { get; }

        public InsertSkillUserCommand(int userId, List<int> skillIds)
        {
            UserId = userId;
            SkillIds = skillIds ?? new List<int>();
        }
    }
}
