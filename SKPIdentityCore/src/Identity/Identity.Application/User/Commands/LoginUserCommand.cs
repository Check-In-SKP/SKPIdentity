using Identity.Domain.Repositories;
using Identity.Domain.Entities;
using MediatR;
using Identity.Application.Common.Services.Interfaces;
using Identity.Application.Common.Exceptions;
using Identity.Application.Session.Commands;
using System.Security.Claims;
using Identity.SharedKernel.Models.Enums;

namespace Identity.Application.User.Commands
{
    public record LoginUserCommand : IRequest<string>
    {
        public required string UsernameOrEmail { get; init; }
        public required string Password { get; init; }
        public required string IpAddress { get; init; }
        public required string UserAgent { get; init; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenService;
        private readonly IMediator _mediator;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenProvider tokenService, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _mediator = mediator;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Identity.Domain.Entities.User? user;

                // Checks if UsernameOrEmail is an email
                if (request.UsernameOrEmail.Contains('@'))
                    user = await _unitOfWork.UserRepository.GetByEmailAsync(request.UsernameOrEmail);
                else
                    user = await _unitOfWork.UserRepository.GetByUsernameAsync(request.UsernameOrEmail);

                // Null check
                if (user == null)
                    throw new ObjectIsNullException("Login failed, user does not exist.");

                // Checks if Password is valid
                if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                    throw new UnauthorizedAccessException("Unauthorized: The provided password did not match.");

                // Calls CreateSessionCommand - The session will be saved to the database within the command
                var createSessionCommand = new CreateSessionCommand
                {
                    IpAddress = request.IpAddress,
                    UserAgent = request.UserAgent,
                    UserId = user.Id
                };

                var sessionDto = await _mediator.Send(createSessionCommand, cancellationToken) ?? throw new ObjectIsNullException("Failed to create a session");

                // Token claims
                var claims = new List<Claim>
                {
                    new("session_id", sessionDto.Id.ToString()),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Name, user.FirstName +  " " + user.LastName),
                };

                // Creates id token with 10 min life span
                var token = await _tokenService.GenerateRsaToken(claims, 10, TokenType.IdToken);

                return token;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedException("Failed to authenticate user: " + ex);
            }
        }
    }
}
