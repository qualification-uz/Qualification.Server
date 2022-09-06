using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Assets;

namespace Qualification.Data.Repositories;

public class FileUploadRepository : IFileUploadRepository
{
    private readonly AppDbContext appDbContext;

    public FileUploadRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IQueryable<Asset> SelectAllAssets() => this.appDbContext.Assets;

    public async ValueTask<Asset> SelectAssetByIdAsync(long assetId) =>
        await this.appDbContext.Assets.FindAsync(assetId);

    public async ValueTask<Asset> InsertAssetAsync(Asset asset)
    {
        EntityEntry<Asset> assetEntityEntry =
            await this.appDbContext.Assets.AddAsync(asset);

        await this.appDbContext.SaveChangesAsync();

        return assetEntityEntry.Entity;
    }

    public async ValueTask<IEnumerable<Asset>> InsertAssetBulkAsync(
        IEnumerable<Asset> assets)
    {
        await this.appDbContext.Assets.AddRangeAsync(assets);
        await this.appDbContext.SaveChangesAsync();

        return assets;
    }

    public async ValueTask<Asset> UpdateAssetAsync(Asset asset)
    {
        EntityEntry<Asset> assetEntityEntry =
            this.appDbContext.Assets.Update(asset);

        await this.appDbContext.SaveChangesAsync();

        return assetEntityEntry.Entity;
    }

    public async ValueTask<Asset> DeleteAssetAsync(Asset asset)
    {
        EntityEntry<Asset> assetEntityEntry =
            this.appDbContext.Assets.Remove(asset);

        await this.appDbContext.SaveChangesAsync();

        return assetEntityEntry.Entity;
    }
}
