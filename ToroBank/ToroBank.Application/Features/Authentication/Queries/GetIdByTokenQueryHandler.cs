using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Users;

namespace ToroBank.Application.Features.Authentication.Queries
{
    public class GetIdByTokenQueryHandler : IRequestHandler<GetIdByTokenQuery, Result<GetIdByTokenDTO>>
    {
        private readonly IAuthRepository _authRepository;

        public GetIdByTokenQueryHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Result<GetIdByTokenDTO>> Handle(GetIdByTokenQuery request, CancellationToken cancellationToken)
        {
            var id = _authRepository.GetIdByToken(request.Token);
            if (id == null)
            {
                throw new NotFoundException("id", id);
            }

            var dto = GetIdByTokenDTO.ToDto(int.Parse($"{(id == null ? 0 : id)}"));

            return Result.Ok(dto);

        }
    }
}
