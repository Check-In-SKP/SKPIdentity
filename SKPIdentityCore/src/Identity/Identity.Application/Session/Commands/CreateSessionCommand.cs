using MediatR;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;

namespace Identity.Application.Session.Commands
{
    public record class CreateSessionCommand : IRequest<Identity.Domain.Entities.Session>
    {
        string IpAddress { get; init; }
        string RefreshToken { get; init; }
        Guid DeviceId { get; init; }
        Guid UserId { get; init; }
    }

    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Identity.Domain.Entities.Session>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSessionCommandHandler(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Identity.Domain.Entities.Session> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = new Identity.Domain.Entities.Session(Guid.NewGuid(), request.RefreshToken, request.IpAddress, request.DeviceId, request.UserId);
            await _sessionRepository.AddAsync(session);
            await _unitOfWork.CommitAsync(cancellationToken);
            return session;
        }
    }
}
