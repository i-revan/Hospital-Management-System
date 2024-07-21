using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.Common.Errors;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.DeleteDepartment;

namespace HospitalManagementsSystem.Application.Tests.CQRS.Commands;

public class DeleteDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly DeleteDepartmentCommandHandler _handler;

    public DeleteDepartmentCommandHandlerTests()
    {
        _departmentService = new Mock<IDepartmentService>();
        _handler = new DeleteDepartmentCommandHandler(_departmentService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnOk_WhenDepartmentIsDeletedSuccessfully()
    {
        var request = new DeleteDepartmentCommandRequest { Id = Guid.NewGuid().ToString() };
        _departmentService.Setup(ds => ds.SoftDeleteDepartmentAsync(request.Id))
            .ReturnsAsync(Result<bool>.Success(true));

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Handle_ShouldReturnOk_WhenDepartmentDeletingFails()
    {
        var request = new DeleteDepartmentCommandRequest { Id = Guid.NewGuid().ToString() };
        _departmentService.Setup(ds => ds.SoftDeleteDepartmentAsync(request.Id))
            .ReturnsAsync(Result<bool>.Failure(DepartmentErrors.DepartmentDeletingFailed));

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteDepartment_CallsServiceOnce()
    {
        var request = new DeleteDepartmentCommandRequest { Id = Guid.NewGuid().ToString() };
        _departmentService.Setup(ds => ds.SoftDeleteDepartmentAsync(request.Id))
            .ReturnsAsync(Result<bool>.Success(true));

        await _handler.Handle(request, It.IsAny<CancellationToken>());

        _departmentService.Verify(ds => ds.SoftDeleteDepartmentAsync(request.Id), Times.Once);
    }
}
