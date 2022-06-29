using AffiliateHub.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AffiliateHub.Application.FileDetails.Dtos;
using AffiliateHub.Application.FileDetails.Commands.UploadFile;
using Microsoft.AspNetCore.StaticFiles;

[Authorize]
public class FileController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<FileDto>> CreateFile(IList<IFormFile> files)
    {
        var file = files.FirstOrDefault();

        using (var fileStream = file!.OpenReadStream())
        {
            var fileDto = await Mediator.Send(new UploadFileCommand
            {
                FileStream = fileStream,
                FileName = file.FileName,
                FileType = AffiliateHub.Domain.Entities.FileType.Image,
                ContentType = file.ContentType,
                FileSize = file.Length
            });

            return Created($"/files/{fileDto.Id}", fileDto);
        }
    }
}