using Identity.Application.Common.Exceptions;
using Identity.Application.Common.Services.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Entities.Enums;
using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.User.Commands
{
    public record CreateUserCommand : IRequest<Guid>
    {
        public string Email { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Password { get; init; } = null!;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBCryptPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IBCryptPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Checks if a user with the same username already exists
            var existingUser = await _unitOfWork.UserRepository.GetByUsernameAsync(request.Username);

            if (existingUser != null)
            {
                if (existingUser.Email == request.Email)
                    throw new ConflictException($"A user with the email '{request.Email}' already exists.");
                else if(existingUser.Username == request.Username)
                    throw new ConflictException($"A user with the username '{request.Username}' already exists.");
            }

            try
            {
                var user = new Domain.Entities.User(
                    Guid.NewGuid(),
                    request.Username,
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    false,
                    false,
                    _passwordHasher.HashPassword(request.Password),
                    new List<int>((int)Role.User));

                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync(cancellationToken);
                return user.Id;
            }
            catch (Exception e)
            {
                throw new ObjectCreationFailedException($"::::: Failed to create user ::::: " + e.Message);
            }
        }
    }
}
