using MediatR;
using ToroBank.Application.Features.Assets;
using ToroBank.WebApi.Helpers;

namespace ToroBank.WebApi.Controllers
{
    public class AssetController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IAssetRepository _userRepository;

        public AssetController(IAssetRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
    }
}
