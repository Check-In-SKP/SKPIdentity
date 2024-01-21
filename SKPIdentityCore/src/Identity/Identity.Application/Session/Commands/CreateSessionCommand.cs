using MediatR;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Application.Common.Services.Interfaces;
using Identity.Application.Session.Dtos;
using AutoMapper;
using Identity.Application.Common.Exceptions;

namespace Identity.Application.Session.Commands
{
    public record class CreateSessionCommand : IRequest<SessionDto>
    {
        public required string IpAddress { get; init; }
        public required string UserAgent { get; init; }
        public required Guid UserId { get; init; }
    }

    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenProvider _tokenProvider;
        private readonly IMapper _mapper;

        public CreateSessionCommandHandler(IUnitOfWork unitOfWork, ITokenProvider tokenProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
        }

        public async Task<SessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var session = new Identity.Domain.Entities.Session(
                    Guid.NewGuid(),
                    _tokenProvider.GenerateRefreshToken(),
                    request.IpAddress,
                    request.UserAgent,
                    request.UserId);

                var sessionDto = _mapper.Map<SessionDto>(session);

                await _unitOfWork.SessionRepository.AddAsync(session);
                await _unitOfWork.CompleteAsync(cancellationToken);
                return sessionDto;
            }
            catch (Exception e)
            {
                throw new ObjectCreationException($"Failed to create a session: {e.Message}");
            }
        }
    }
}
