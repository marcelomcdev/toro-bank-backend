using MediatR;
using ToroBank.Application.Common.Wrappers;

namespace ToroBank.Application.Features.Positions.Queries
{
    public class GetUserPositionQuery : PagedRequest, IRequest<Result<GetUserPositionDTO>>
    {
        public int Id { get; set; }
    }

    

}
