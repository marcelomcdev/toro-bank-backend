using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.Positions.Queries;
using ToroBank.WebApi.Helpers;

namespace ToroBank.WebApi.Controllers
{
    [Route("userPosition")]
    public class UserPositionController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IAssetRepository _userRepository;

        public UserPositionController(IAssetRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Get a view of balance, investments and total equity in Toro.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetUserPositionQuery() { Id = id, PageNumber = 1, PageSize = 100});
            return Ok(response.Data);
        }
    }
}
