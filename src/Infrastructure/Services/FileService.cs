// https://codewithmukesh.com/blog/working-with-aws-s3-using-aspnet-core/#Creating_User_Generating_Access_Keys_via_AWS_IAM

using AffiliateHub.Application.Common.Interfaces;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AffiliateHub.Infrastructure.Services;

public class FileService : IFileService
{

    private readonly IAmazonS3 _s3Client;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly string BUCKET_NAME;

    public FileService(IAmazonS3 s3Client, ILogger<FileService> logger, IConfiguration configuration, IEnvironment env)
    {
        _s3Client = s3Client;
        _logger = logger;
        _configuration = configuration;
        BUCKET_NAME = _configuration.GetValue<string>("AWS:BucketName");
    }

    public async Task DeleteFileAsync(string name)
    {
        var result = await _s3Client.DeleteObjectAsync(BUCKET_NAME, name);
    }

    public string GetFile(string name)
    {
        var urlRequest = new GetPreSignedUrlRequest()
        {
            BucketName = BUCKET_NAME,
            Key = name,
            Expires = DateTime.UtcNow.AddMinutes(1)
        };
        var presignedUrl = _s3Client.GetPreSignedURL(urlRequest);
        return presignedUrl;
    }

    public async Task<FileStoreInfo> UploadFileAsync(string name, Stream stream, string contentType)
    {
        Console.WriteLine($"Bucket Name {BUCKET_NAME}.");
        var request = new PutObjectRequest()
        {
            BucketName = BUCKET_NAME,
            Key = name,
            InputStream = stream
        };
        request.Metadata.Add("Content-Type", contentType);
        var putObjectResult = await _s3Client.PutObjectAsync(request);

        return new FileStoreInfo
        {
            Url = GetFile(name),
            BucketName = BUCKET_NAME
        };
    }
}
