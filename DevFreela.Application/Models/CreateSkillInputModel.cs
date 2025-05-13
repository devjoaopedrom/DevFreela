using DevFreela.Core.Entities;
using System.Net.Sockets;

namespace DevFreela.Application.Models
{
    public class CreateSkillInputModel
    {
        public string Description { get; set; }

        public CreateSkillInputModel( string description)
        {
            Description = description;
        }
        public Skill ToEntity()
          => new(Description);
    }
}
