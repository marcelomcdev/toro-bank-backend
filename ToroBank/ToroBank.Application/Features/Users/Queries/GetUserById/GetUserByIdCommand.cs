using MediatR;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdCommand : IRequest<Result<User>>
    {
        public int Id { get; set; }
    }
}
