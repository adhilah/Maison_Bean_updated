namespace MaisonBean.Application.Interfaces;

public interface IImageService
{
    Task<(string ImageUrl, string PublicId)>
    UploadImageAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default
    );
}