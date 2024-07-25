namespace HospitalManagementSystem.Application.Abstraction.Services.Storage;

public interface IBlobService
{
    Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken=default);

    Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken=default);

    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken=default);
}
