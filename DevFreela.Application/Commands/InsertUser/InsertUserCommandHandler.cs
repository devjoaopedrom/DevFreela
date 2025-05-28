using DevFreela.Application.Notification.UserCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        
        private readonly IUserRepository _repository;
        private readonly IMediator _mediator;
        public InsertUserCommandHandler(IUserRepository repository, IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await _repository.AddUser(user);
            
            var userCreated = new UserCreatedNotification(user.FullName, user.Email);
            await _mediator.Publish(userCreated);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
