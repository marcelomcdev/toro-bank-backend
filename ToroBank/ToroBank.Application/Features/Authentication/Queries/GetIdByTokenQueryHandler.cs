using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Common.Identity.Services;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Users;

namespace ToroBank.Application.Features.Authentication.Queries
{
    public class GetIdByTokenQueryHandler : IRequestHandler<GetIdByTokenQuery, Result<GetIdByTokenDTO>>
    {
        
        private readonly ITokenService _tokenService;

        public GetIdByTokenQueryHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<Result<GetIdByTokenDTO>> Handle(GetIdByTokenQuery request, CancellationToken cancellationToken)
        {
            var id = _tokenService.GetIdByToken(request.Token);
            if (id == null)
            {
                throw new NotFoundException("id", id);
            }

            var dto = GetIdByTokenDTO.ToDto(int.Parse($"{(id?.Result == null ? 0 : id?.Result)}"));

            return Result.Ok(dto);

        }
    }
}
