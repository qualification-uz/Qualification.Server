using AutoMapper;
using Microsoft.AspNetCore.Http;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Assets;
using Qualification.Service.DTOs;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IAssetService assetService;
    private readonly IFileUploadRepository fileUploadRepository;
    private readonly IMapper mapper;

    public FileUploadService(
        IAssetService assetService,
        IFileUploadRepository fileUploadRepository,
        IMapper mapper)
    {
        this.assetService = assetService;
        this.fileUploadRepository = fileUploadRepository;
        this.mapper = mapper;
    }

    public async ValueTask<AssetDto> RetrieveFileByIdAsync(long fileId)
    {
        var asset = await this.fileUploadRepository
            .SelectAssetByIdAsync(fileId);

        if (asset is null)
            throw new NotFoundException("Couldn't find asset for given id");

        return this.mapper.Map<AssetDto>(asset);
    }

    public async ValueTask<IEnumerable<AssetDto>> SaveFilesAsync(
        IReadOnlyList<IFormFile> formFiles)
    {
        var assets = new List<Asset>();

        foreach (var file in formFiles)
        {
            (var fileName, var filePath) = await this.assetService
                .SaveFileAsync(file);

            assets.Add(new Asset { Url = fileName });
        }

        await this.fileUploadRepository
            .InsertAssetBulkAsync(assets);

        return this.mapper.Map<IEnumerable<AssetDto>>(assets);
    }

    public async ValueTask<AssetDto> ModifyFileAsync(
        long fileId, IFormFile formFile)
    {
        var asset = await this.fileUploadRepository
            .SelectAssetByIdAsync(fileId);

        if (asset is null)
            throw new NotFoundException("Couldn't find asset for given id");

        assetService.DeleteFile(asset.Url);

        (var fileName, var filePath) = await this.assetService
            .SaveFileAsync(formFile);

        asset.Url = fileName;
        asset.UpdatedAt = DateTime.UtcNow;

        asset = await this.fileUploadRepository
            .UpdateAssetAsync(asset);

        return this.mapper.Map<AssetDto>(asset);
    }

    public async ValueTask<AssetDto> RemoveFileAsync(long fileId)
    {
        var asset = await this.fileUploadRepository
            .SelectAssetByIdAsync(fileId);

        if (asset is null)
            throw new NotFoundException("Couldn't find asset for given id");

        assetService.DeleteFile(asset.Url);

        asset = await this.fileUploadRepository
            .DeleteAssetAsync(asset);

        return this.mapper.Map<AssetDto>(asset);
    }
}
