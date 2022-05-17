using MediatR;
using ToroBank.Application.Common.Wrappers;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.UserAssets;
using ToroBank.Application.Features.Users;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.PurchaseOrder.Commands
{
    public class SubmitPurchaseOrderHandler : IRequestHandler<SubmitPurchaseOrderCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IUserAssetRepository _userAssetRepository;


        public SubmitPurchaseOrderHandler(IUserRepository userRepository, IAssetRepository assetRepository, IUserAssetRepository userAssetRepository)
        {
            _userRepository = userRepository;
            _assetRepository = assetRepository;
            _userAssetRepository = userAssetRepository;
        }

        public async Task<Result<string>> Handle(SubmitPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var userAssets = await _userAssetRepository.GetAssetsByUserAsync(request.UserId);
            var asset = await _assetRepository.GetAssetBySymbol(request.Symbol);
            UserAsset? userAssetToInsertOrUpdate;

            if(asset != null && userAssets.Any(f=> f.AssetId == asset.Id))
            {
                userAssetToInsertOrUpdate = userAssets.FirstOrDefault(f => f.Id == asset.Id);

                if(userAssetToInsertOrUpdate == null)
                {
                    await _userAssetRepository.AddAsync(new UserAsset(asset.Id, user.Id, request.Amount));
                }
                else
                {
                    userAssetToInsertOrUpdate.Quantity += request.Amount;
                    await _userAssetRepository.UpdateAsync(userAssetToInsertOrUpdate);
                }
            }
            else
            {
                userAssetToInsertOrUpdate = new UserAsset(asset.Id, user.Id, request.Amount);
                await _userAssetRepository.AddAsync(userAssetToInsertOrUpdate);
            }
            
            user.Balance -= (asset.Value * request.Amount);
            await _userRepository.UpdateAsync(user);
            return Result<string>.Ok("Compra realizada com sucesso!");
        }
    }

}
