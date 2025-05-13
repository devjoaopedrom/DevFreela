using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<ResultViewModel>
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
