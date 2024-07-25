using HospitalManagementSystem.Application.Abstraction.Services.Storage;

namespace HospitalManagementSystem.API.Controllers.v1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IBlobService _blobService;

    public FilesController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpPost("files")]
    public async Task<IActionResult> Post(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        Guid fileId = await _blobService.UploadAsync(stream, file.ContentType);

        return Ok(fileId);
    }

    [HttpGet("files/{fileId}")]
    public async Task<IActionResult> Get(Guid fileId)
    {
        FileResponse file = await _blobService.DownloadAsync(fileId);

        return File(file.Stream, file.ContentType);
    }

    [HttpDelete("files/{fileId}")]
    public async Task<IActionResult> Delete(Guid fileId)
    {
        await _blobService.DeleteAsync(fileId);

        return NoContent();
    }
}
