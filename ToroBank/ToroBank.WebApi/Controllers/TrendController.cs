using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.Assets.Queries.GetMostTradedAssets;
using ToroBank.WebApi.Helpers;

namespace ToroBank.WebApi.Controllers
{
    [Route("trends")]
    public class TrendController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IAssetRepository _userRepository;

        public TrendController(IAssetRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] GetMostTradedAssetsQuery cmd)
        {
            var response = await _mediator.Send(cmd);
            List<dynamic> l = new List<dynamic>();
            response.Data.Data.ToList()
                .ForEach(response => l.Add(new { 
                    symbol = response.Asset.Name, 
                    currentPrice = response.Asset.Value,
                    id = response.Asset.Id,
                    image = response.Asset.ImageName
                } ));
            return Ok(l);
            //return Ok(response.Data);
        }

        

    }
}
