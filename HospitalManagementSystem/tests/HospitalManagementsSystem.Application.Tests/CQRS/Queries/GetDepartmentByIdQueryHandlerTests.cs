using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
using HospitalManagementSystem.Application.DTOs.Departments;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementsSystem.Application.Tests.CQRS.Queries;

public class GetDepartmentByIdQueryHandlerTests
{
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetDepartmentByIdQueryHandler _handler;

    public GetDepartmentByIdQueryHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _departmentService = new Mock<IDepartmentService>();
        _handler = new GetDepartmentByIdQueryHandler(_departmentService.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDepartment_WhenDataExists()
    {
        var departmentId = Guid.NewGuid();
        List<Doctor> doctors = new List<Doctor>
        {
            new() { Name = "xxx", Surname = "xxx", Address = "xxx", Phone = "+9942311212", DepartmentId = departmentId},
            new() { Name = "xxx", Surname = "xxx", Address = "xxx", Phone = "+9942311212", DepartmentId = departmentId}
        };
        var departmentDto = new DepartmentItemDto { Id = Guid.NewGuid().ToString(), Name = "Test", Doctors =  doctors};
        var expectedResponse = new GetDepartmentByIdQueryResponse(new DepartmentItemDto { 
            Id = departmentId.ToString(), Name = "DepartmentTest", Doctors = doctors });

        _departmentService.Setup(s => s.GetByIdAsync(departmentId.ToString()))
            .ReturnsAsync(Result<DepartmentItemDto>.Success(departmentDto));
        _mapperMock.Setup(m => m.Map<GetDepartmentByIdQueryResponse>(departmentDto)).Returns(expectedResponse);

        var request = new GetDepartmentByIdQueryRequest { Id = departmentId.ToString() };

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        Assert.NotNull(response.Value);
        Assert.Equal(expectedResponse.Department.Id, response.Value.Department.Id);
        Assert.Equal(expectedResponse.Department.Name, response.Value.Department.Name);
    }

    [Fact]
    public async Task Handle_ReturnsNullResponse_WhenDepartmentDoesNotExist()
    {
        var departmentId = Guid.NewGuid();
        DepartmentItemDto nullDepartment = null; // Simulate null department

        _departmentService.Setup(s => s.GetByIdAsync(departmentId.ToString()))
            .ReturnsAsync(Result<DepartmentItemDto>.Success(nullDepartment));

        var request = new GetDepartmentByIdQueryRequest { Id = departmentId.ToString() };

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        Assert.Null(response.Value);
    }
}
