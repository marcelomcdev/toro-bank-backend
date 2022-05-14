using MediatR;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<GetUserByIdDTO>>
    {
        public int Id { get; set; }
    }
}
