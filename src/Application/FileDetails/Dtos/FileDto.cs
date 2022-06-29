using AffiliateHub.Application.Common.Mappings;
using AffiliateHub.Domain.Entities;

namespace AffiliateHub.Application.FileDetails.Dtos
{
    public class FileDto : IMapFrom<FileDetail>
    {
        public string Id { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public FileType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BlurHash { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}