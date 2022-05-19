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
            var allUserAssets = await _userAssetRepository.GetAllAsync();
            var assetsOfUser = allUserAssets.Where(f => f.UserId == user.Id).ToList();
            var groupAssetsOfUser = assetsOfUser.GroupBy(f => new { f.UserId, f.AssetId });

            UserPosition up = new UserPosition();

            groupAssetsOfUser.ToList().ForEach(f => up.Positions.Add(new Position()
            {
                Amount = f.Sum(f => f.Quantity),
                Symbol = assets.FirstOrDefault(g => g.Id == f.Key.AssetId)?.Name ?? "",
                CurrentPrice = assets.FirstOrDefault(g => g.Id == f.Key.AssetId)?.Value ?? 0,
                Image = assets.FirstOrDefault(g => g.Id == f.Key.AssetId)?.ImageName ?? ""
            })); 

            up.CheckingAccountAmount = user.Balance;
            assetsOfUser.ForEach(f => up.Investments += ((f.Quantity * assets.FirstOrDefault(e => e.Id == f.AssetId)?.Value ?? 0)));
            up.Consolidated = (up.CheckingAccountAmount + up.Investments);

            GetUserPositionDTO dto = GetUserPositionDTO.ToDto(up);
            return Result.Ok(dto);
        }
    }
}
