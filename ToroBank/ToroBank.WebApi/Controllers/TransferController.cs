using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Transfer.Commands.ReceiveTransfer;
using ToroBank.Application.Features.Users;

namespace ToroBank.WebApi.Controllers
{
    [Route("api/[controller]/spb")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public TransferController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Transfer event notification.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost("events")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTransferEvent([FromBody] ReceiveTransferCommand cmd)
        {

            var response = await _mediator.Send(cmd);
            return Created($"/api/[controller]/{response.Data}", null);
        }
    }
}
