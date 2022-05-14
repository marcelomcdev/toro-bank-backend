using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<GetUserByIdDTO>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), query.Id);
            }

            var dto = GetUserByIdDTO.ToDto(user);

            return Result.Ok(dto);
        }
    }
}
