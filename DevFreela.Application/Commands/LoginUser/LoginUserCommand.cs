using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<ResultViewModel<LoginViewModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
