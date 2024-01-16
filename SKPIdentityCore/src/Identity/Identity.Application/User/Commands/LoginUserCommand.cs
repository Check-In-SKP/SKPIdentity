using Identity.Domain.Repositories;
using Identity.Domain.Entities;
using MediatR;
using Identity.Application.Common.Services.Interfaces;

namespace Identity.Application.User.Commands
{
    public record LoginUserCommand : IRequest<string>
    {
        public required string UsernameOrEmail { get; init; }
        public required string Password { get; init; }
        public required string IpAddress { get; init; }
        public Guid DeviceId { get; init; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBCryptPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IBCryptPasswordHasher passwordHasher, ITokenProvider tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async  Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Identity.Domain.Entities.User? user;

                // Checks if UsernameOrEmail is an email
                if (request.UsernameOrEmail.Contains('@'))
                {
                    user = await _unitOfWork.UserRepository.GetByEmailAsync(request.UsernameOrEmail);
                }
                else
                {
                    user = await _unitOfWork.UserRepository.GetByUsernameAsync(request.UsernameOrEmail);
                }

                if (user == null)
                {
                    // Implement null exception
                    throw new ArgumentException();
                }

                // Checks if Password is valid
                if(!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                {
                    // Implement invalid password exception
                    throw new Exception("Invalid password");
                }

                // Generate authentiaction code

            }
            catch (Exception ex)
            {

            }

            return "";
        }
    }
}
