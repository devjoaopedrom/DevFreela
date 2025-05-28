using MediatR;
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillCommandHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        private readonly ISkillRepository _repository;

        public InsertSkillCommandHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
           var skill =  request.ToEntity();

            await _repository.AddSkill(skill);

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
