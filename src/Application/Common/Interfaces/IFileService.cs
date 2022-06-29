
namespace AffiliateHub.Application.Common.Interfaces;

public class FileStoreInfo
{
    public string Url { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
}

public interface IFileService
{
    Task<FileStoreInfo> UploadFileAsync(string name, Stream stream, string contentType);
    string GetFile(string name);
    Task DeleteFileAsync(string name);
}