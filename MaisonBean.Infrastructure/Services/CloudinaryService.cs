using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using MaisonBean.Application.Interfaces;
using MaisonBean.Infrastructure.Configurations;

using Microsoft.Extensions.Options;

namespace MaisonBean.Infrastructure.Services;

public class CloudinaryService : IImageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(
        IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(
            settings.CloudName,
            settings.ApiKey,
            settings.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<(string ImageUrl, string PublicId)>
    UploadImageAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        if (stream == null || stream.Length == 0)
        {
            throw new Exception(
                "Image stream is empty."
            );
        }

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(
                fileName,
                stream
            ),

            Folder = "maisonbean/products"
        };

        var result =
            await _cloudinary.UploadAsync(
                uploadParams,
                cancellationToken
            );

        if (result.Error != null)
        {
            throw new Exception(
                result.Error.Message
            );
        }

        return (
            result.SecureUrl.ToString(),
            result.PublicId
        );
    }

    public async Task DeleteImageAsync(
        string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
        {
            return;
        }

        var deleteParams =
            new DeletionParams(publicId);

        var result =
            await _cloudinary.DestroyAsync(
                deleteParams
            );

        if (result.Error != null)
        {
            throw new Exception(
                result.Error.Message
            );
        }
    }
}