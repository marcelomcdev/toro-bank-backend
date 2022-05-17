using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.PurchaseOrder.Commands;
using ToroBank.WebApi.Helpers;

namespace ToroBank.WebApi.Controllers
{

    [Route("order")]
    public class PurchaseOrderController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IAssetRepository _userRepository;

        public PurchaseOrderController(IAssetRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseOrder([FromBody] SubmitPurchaseOrderCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return Created($"/[controller]/{response.Data}", response.Data);
        }
    }
}
