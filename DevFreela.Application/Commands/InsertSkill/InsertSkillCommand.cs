using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel<int>> 
    {
        public InsertSkillCommand(int id, string description)
        {
            Id = id;
            Description = description;
        }
        public int Id { get; set; }
        public string Description { get; set; }

        public Skill ToEntity()
            => new(Description);
    }
}
