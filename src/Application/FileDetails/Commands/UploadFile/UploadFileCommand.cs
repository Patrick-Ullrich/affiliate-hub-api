using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Domain.Entities;
using AutoMapper;
using MediatR;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Blurhash.ImageSharp;
using AffiliateHub.Application.FileDetails.Dtos;

namespace AffiliateHub.Application.FileDetails.Commands.UploadFile;

public record UploadFileCommand : IRequest<FileDto>
{
    public Stream FileStream { get; set; } = Stream.Null;
    public string FileName { get; set; } = string.Empty;
    public FileType FileType { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
}

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, FileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUserService;

    public UploadFileCommandHandler(IApplicationDbContext context, IFileService fileService, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _fileService = fileService;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<FileDto> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId == null)
        {
            throw new UnauthorizedAccessException();
        }

        string blurHash = string.Empty;
        // Generate Blur Code
        using (var image = Image.Load<Rgb24>(request.FileStream))
        {
            image.Mutate(i => i.Resize(32, 32));
            blurHash = Blurhasher.Encode(image, 4, 3);
        }

        var test = Path.ChangeExtension(request.FileName, "webp");

        var webpImageStream = new MemoryStream();
        request.FileStream.Position = 0;
        using (var image = Image.Load(request.FileStream))
        {
            image.SaveAsWebp(webpImageStream);
        }

        var fileDetailId = Guid.NewGuid().ToString();
        var fileDetail = new FileDetail
        {
            Id = fileDetailId,
            MimeType = "image/webp",
            FileSize = request.FileSize,
            FilePath = $"{fileDetailId}-{test}",
            Name = request.FileName,
            BlurHash = blurHash,
            BucketName = string.Empty,
            Type = request.FileType,
            UserId = _currentUserService.UserId,
        };

        var fileInfo = await _fileService.UploadFileAsync(fileDetail.FilePath, webpImageStream, request.ContentType);
        fileDetail.BucketName = fileInfo.BucketName;
        _context.FileDetails.Add(fileDetail);

        await _context.SaveChangesAsync(cancellationToken);
        var fileDto = _mapper.Map(fileDetail, new FileDto());
        fileDto.Url = fileInfo.Url;
        return fileDto;
    }
}
