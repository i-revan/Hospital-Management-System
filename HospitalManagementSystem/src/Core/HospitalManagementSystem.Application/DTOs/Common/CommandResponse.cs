using System.Net;

namespace HospitalManagementSystem.Application.DTOs.Common;
public record CommandResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public string? Message { get; init; }
}


