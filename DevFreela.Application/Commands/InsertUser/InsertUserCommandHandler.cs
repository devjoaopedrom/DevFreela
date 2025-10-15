using DevFreela.Application.Notification.UserCreated;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IAuthService _auth;
        private readonly IUserRepository _repository;
        private readonly IMediator _mediator;
        public InsertUserCommandHandler(IUserRepository repository, IMediator mediator, IAuthService auth)
        {
            _mediator = mediator;
            _repository = repository;
            _auth = auth;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _auth.ComputeHash(request.Password);
            var user = request.ToEntity(hashedPassword);

            await _repository.AddUser(user);
            
            var userCreated = new UserCreatedNotification(user.FullName, user.Email);
            await _mediator.Publish(userCreated);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
