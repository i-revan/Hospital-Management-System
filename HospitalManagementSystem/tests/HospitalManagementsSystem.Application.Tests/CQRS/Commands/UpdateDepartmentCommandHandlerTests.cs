using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
using HospitalManagementSystem.Application.DTOs.Departments;

namespace HospitalManagementsSystem.Application.Tests.CQRS.Commands;

public class UpdateDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateDepartmentCommandHandler _handler;

    public UpdateDepartmentCommandHandlerTests()
    {
        _departmentService = new Mock<IDepartmentService>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateDepartmentCommandHandler(_departmentService.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnOk_WhenDepartmentIsUpdatedSuccessfully()
    {
        var request = new UpdateDepartmentCommandRequest(Guid.NewGuid().ToString(), "Update Test");
        var departmentDto = new DepartmentUpdateDto(request.Name);

        _mapperMock.Setup(m=>m.Map<DepartmentUpdateDto>(request)).Returns(departmentDto);
        _departmentService.Setup(ds => ds.UpdateDepartmentAsync(request.Id,departmentDto)).ReturnsAsync(true);

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenDepartmentUpdatingFails()
    {
        var request = new UpdateDepartmentCommandRequest(Guid.NewGuid().ToString(), "Update Test");
        var departmentDto = new DepartmentUpdateDto(request.Name);

        _mapperMock.Setup(m => m.Map<DepartmentUpdateDto>(request)).Returns(departmentDto);
        _departmentService.Setup(ds => ds.UpdateDepartmentAsync(request.Id, departmentDto)).ReturnsAsync(false);

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateDepartment_ValidDto_CallsServiceOnce()
    {
        var request = new UpdateDepartmentCommandRequest(Guid.NewGuid().ToString(), "Update Test");
        var departmentDto = new DepartmentUpdateDto(request.Name);

        _mapperMock.Setup(m => m.Map<DepartmentUpdateDto>(request)).Returns(departmentDto);
        _departmentService.Setup(ds => ds.UpdateDepartmentAsync(request.Id, departmentDto)).ReturnsAsync(true);

        await _handler.Handle(request, It.IsAny<CancellationToken>());

        _departmentService.Verify(ds => ds.UpdateDepartmentAsync(request.Id,departmentDto), Times.Once);
    }
}
