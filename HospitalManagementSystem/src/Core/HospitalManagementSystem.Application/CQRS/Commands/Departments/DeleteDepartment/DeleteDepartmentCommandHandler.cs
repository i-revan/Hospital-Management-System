﻿namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.DeleteDepartment;
public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommandRequest, DeleteDepartmentCommandResponse>
{
    private readonly IDepartmentService _departmentService;

    public DeleteDepartmentCommandHandler(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    public async Task<DeleteDepartmentCommandResponse> Handle(DeleteDepartmentCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _departmentService.SoftDeleteDepartmentAsync(request.Id);
        return new DeleteDepartmentCommandResponse
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? "Department is successfully deleted" : result.Error.Description
        };
    }
}

