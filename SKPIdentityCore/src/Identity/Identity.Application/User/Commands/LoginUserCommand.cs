using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.User.Commands
{
    public record LoginUserCommand : IRequest<string>
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string RedirectUri { get; init; }
        public string CodeChallenge { get; init; }
    }
}
