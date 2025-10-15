using DevFreela.Application;
using DevFreela.Application.Commands.InsertSkillUser;
using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly IAuthService _auth;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        public UsersController(IMediator mediator, IAuthService auth,
            IMemoryCache cache,
            IEmailService emailService,
            DevFreelaDbContext context)
        {
            _mediator = mediator;
            _auth = auth;
            _cache = cache;
            _emailService = emailService;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // POST api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task <IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(InsertSkillUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess) 
            {
                BadRequest(result.Message);
            }
            return NoContent();
        }
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputModel model) 
        {
            var command = new LoginUserCommand
            {
                Email = model.Email,
                Password = model.Password
             };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                var error = ResultViewModel<LoginViewModel>.Error("Erro de Login"); 
                return BadRequest(error);
            }
            return Ok(result.Data);
        }

        [HttpPost("password-recovery/request")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryRequestInputModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                var error = ResultViewModel<string>.Error("O email nao existe");
                return BadRequest(error);
            }

            var code = new Random().Next(100000, 999999).ToString();

            var cacheKey = $"RecoveryCode:{model.Email}";

            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(user.Email, "Codigo de Recuperação", $"Seu código de recuperação é: {code}");

            return NoContent();
        }
        
        [HttpPost("password-recovery/validate")]
        [AllowAnonymous]
        public IActionResult ValidateRecoveryCode(ValidateRecoveryCodeInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";

            if (!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
            {
                return BadRequest();
            }

            return NoContent();
        }
        
        [HttpPost("password-recovery/change")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordInputModel model)
        {
            var cacheKey = $"RecoveryCode:{model.Email}";
            
            if (!_cache.TryGetValue(cacheKey, out string? code) || code != model.Code)
            {
                return BadRequest();
            }

            _cache.Remove(cacheKey);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                var error = ResultViewModel <string>.Error("O email nao existe");
                return BadRequest(error);
            }


            var hash = _auth.ComputeHash(model.NewPassword);
            user.UpdatePassword(hash);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
