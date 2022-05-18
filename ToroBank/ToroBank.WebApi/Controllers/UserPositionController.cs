using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.Assets.Queries.GetMostTradedAssets;
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
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromBody] GetMostTradedAssetsQuery cmd)
        {
            var response = await _mediator.Send(cmd);
            return Ok(response.Data);
        }

        
    }
}
