using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewModel>>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _auth;
        public LoginUserCommandHandler(IUserRepository repository, IAuthService auth)
        {
            _repository = repository;
            _auth = auth;
        }

        public async Task<ResultViewModel<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _auth.ComputeHash(request.Password);

            var user = await _repository.GetByEmailAndPasswordAsync(request.Email, hashedPassword);

            if (user == null)
                return ResultViewModel<LoginViewModel>.Error("Usuário ou senha inválidos.");

            var token = _auth.GenerateToken(user.Email, user.Role);

            var viewModel = new LoginViewModel(token);

            return ResultViewModel<LoginViewModel>.Success(viewModel);
        }
    }
}
