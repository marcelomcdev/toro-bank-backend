using MediatR;
using ToroBank.Application.Common.Exceptions;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.UserAssets;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Positions.Queries
{
    public class GetUserPositionQueryHandler : IRequestHandler<GetUserPositionQuery, Result<GetUserPositionDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IUserAssetRepository _userAssetRepository;

        public GetUserPositionQueryHandler(IUserRepository userRepository, IAssetRepository assetRepository, IUserAssetRepository userAssetRepository)
        {
            _userRepository = userRepository;
            _assetRepository = assetRepository;
            _userAssetRepository = userAssetRepository;
        }

        public async Task<Result<GetUserPositionDTO>> Handle(GetUserPositionQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var assets = await _assetRepository.GetAllAsync();
            var assetsOfUser = _userAssetRepository.GetAllAsync().Result.Where(f => f.Id == user.Id).ToList();

            UserPosition up = new UserPosition();
            assetsOfUser.ForEach(f => up.Positions.Add(new Position()
            {
                Amount = f.Quantity,
                Symbol = assets.FirstOrDefault(g => g.Id == f.AssetId)?.Name ?? "",
                CurrentPrice = assets.FirstOrDefault(g => g.Id == f.AssetId)?.Value ?? 0
            }));

            up.CheckingAccountAmount = user.Balance;
            assetsOfUser.ForEach(f => up.Consolidated += ((f.Quantity * assets.FirstOrDefault(e => e.Id == f.AssetId)?.Value ?? 0)));
            up.Consolidated += up.CheckingAccountAmount;

            GetUserPositionDTO dto = GetUserPositionDTO.ToDto(up);
            return Result.Ok(dto);
        }
    }
}
