using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Authentication.Queries
{
    public class GetIdByTokenQuery : IRequest<Result<GetIdByTokenDTO>>
    {
        public string Token { get; set; }
    }
}
