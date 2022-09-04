using Qualification.Domain.Entities.Assets;

namespace Qualification.Data.IRepositories;

public interface IFileUploadRepository
{
    IQueryable<Asset> SelectAllAssets();
    ValueTask<Asset> SelectAssetByIdAsync(long assetId);
    ValueTask<Asset> InsertAssetAsync(Asset asset);
    ValueTask<IEnumerable<Asset>> InsertAssetBulkAsync(IEnumerable<Asset> assets);
    ValueTask<Asset> UpdateAssetAsync(Asset asset);
    ValueTask<Asset> DeleteAssetAsync(Asset asset);
}
