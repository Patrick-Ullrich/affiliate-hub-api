using Amazon.S3;
using Amazon.S3.Model;
using Blurhash.ImageSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace AffiliateHub.WebUI.Controllers;

public class TestController : ApiControllerBase
{

    public class S3ObjectDto
    {
        public string? Name { get; set; }
        public string? PresignedUrl { get; set; }
        public string? blurHash { get; set; }
    }

    public IAmazonS3 _s3Client;

    public TestController(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Upload(List<IFormFile> files)
    {
        return "string";
        // var bucketName = "affiliate-hub-testing";

        // string blurCode = string.Empty;
        // var file = files.FirstOrDefault();

        // using (var fileStream = file!.OpenReadStream())
        // {
        //     using (var image = Image.Load<Rgb24>(fileStream))
        //     {
        //         image.Mutate(i => i.Resize(32, 32));
        //         blurCode = Blurhasher.Encode(image, 4, 3);
        //     }
        // }

        // var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
        // if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
        // var request = new ListObjectsV2Request()
        // {
        //     BucketName = bucketName
        // };
        // var result = await _s3Client.GetObjectAsync(bucketName, "test.jpg"); //.ListObjectsV2Async(request);
        // var s3Objects = result.Select(s =>
        // {
        //     var urlRequest = new GetPreSignedUrlRequest()
        //     {
        //         BucketName = bucketName,
        //         Key = s.Key,
        //         Expires = DateTime.UtcNow.AddMinutes(1)
        //     };
        //     return new S3ObjectDto()
        //     {
        //         Name = s.Key.ToString(),
        //         PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
        //         blurHash = blurCode
        //     };
        // });
        // return Ok(s3Objects);



        // MemoryStream result = new MemoryStream();
        // IImageFormat format = null;

        // long size = files.Sum(f => f.Length);
        // var file = files.FirstOrDefault();
        // using (var fileStream = file!.OpenReadStream())
        // {



        //     using (var image = Image.Load(fileStream, out format))
        //     {
        //         await image.SaveAsWebpAsync(result);
        //     }
        // }

        // result.Position = 0;
        // return new FileStreamResult(result, format.DefaultMimeType);
    }
}