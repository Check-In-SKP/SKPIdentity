using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Session.Dtos
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string? RefreshToken { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Session, SessionDto>();
            }
        }
    }
}
