using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetAllDepartments;
using HospitalManagementSystem.Application.DTOs.Departments;

namespace HospitalManagementsSystem.Application.Tests.CQRS.Queries;

public class GetAllDepartmentsQueryHandlerTests
{
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllDepartmentsQueryHandler _handler;

    public GetAllDepartmentsQueryHandlerTests()
    {
        _departmentService = new Mock<IDepartmentService>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllDepartmentsQueryHandler(_departmentService.Object, _mapperMock.Object);
    }
    [Fact]
    public async Task Handle_ReturnsAllDepartments_WhenDataExists()
    {
        var departmentDtos = new List<AllDepartmentsDto>
            {
                new AllDepartmentsDto { Id = Guid.NewGuid().ToString(), Name = "DepartmentTest" },
                new AllDepartmentsDto { Id = Guid.NewGuid().ToString(), Name = "DepartmentNew" }
            };

        var expectedResponse = new GetAllDepartmentsQueryResponse(
            new List<AllDepartmentsDto>
                {
                    new AllDepartmentsDto { Id = Guid.NewGuid().ToString(), Name = "DepartmentTest" },
                    new AllDepartmentsDto { Id = Guid.NewGuid().ToString(), Name = "DepartmentNew" }
                }
        );

        _departmentService.Setup(s => s.GetAllAsync()).ReturnsAsync(departmentDtos);
        _mapperMock.Setup(m => m.Map<GetAllDepartmentsQueryResponse>(departmentDtos)).Returns(expectedResponse);

        var request = new GetAllDepartmentsQueryRequest();

        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        Assert.NotNull(response);
        Assert.Equal(expectedResponse, response);
        Assert.Equal(expectedResponse.Departments.Count, response.Departments.Count);
    }

    [Fact]
    public async Task Handle_ReturnsEmpty_WhenNoDataExists()
    {
        var departmentDtos = new List<AllDepartmentsDto>();
        var expectedResponse = new GetAllDepartmentsQueryResponse(new List<AllDepartmentsDto>());

        _departmentService.Setup(s => s.GetAllAsync()).ReturnsAsync(departmentDtos);
        _mapperMock.Setup(m => m.Map<GetAllDepartmentsQueryResponse>(departmentDtos)).Returns(expectedResponse);

        var request = new GetAllDepartmentsQueryRequest();

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Empty(response.Departments);
    }
}
