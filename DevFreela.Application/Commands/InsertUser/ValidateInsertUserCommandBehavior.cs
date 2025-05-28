using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class ValidateInsertUserCommandBehavior : IPipelineBehavior<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;

        public ValidateInsertUserCommandBehavior(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {

            var emailExists = _context.Users.Any(e => e.Email == request.Email);
            

            if (emailExists  is true)
            {
                return ResultViewModel<int>.Error("Este email ja possui um cadastro");
            }
            return await next();
        }
    }
 }

