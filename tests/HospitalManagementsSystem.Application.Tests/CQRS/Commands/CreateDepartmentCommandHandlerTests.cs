using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.CreateDepartment;
using HospitalManagementSystem.Application.DTOs.Departments;
namespace HospitalManagementsSystem.Application.Tests.CQRS.Commands;

public class CreateDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateDepartmentCommandHandler _handler;

    public CreateDepartmentCommandHandlerTests()
    {
        _departmentService = new Mock<IDepartmentService>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateDepartmentCommandHandler(_departmentService.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCreated_WhenDepartmentIsCreatedSuccessfully()
    {
        //Arrange
        var request = new CreateDepartmentCommandRequest { Name = "Test Department" };
        var departmentDto = new DepartmentCreateDto (request.Name);

        _mapperMock.Setup(m => m.Map<DepartmentCreateDto>(request)).Returns(departmentDto);
        _departmentService.Setup(ds => ds.CreateDepartmentAsync(departmentDto)).ReturnsAsync(true);

        //Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenDepartmentCreationFails()
    {
        // Arrange
        var request = new CreateDepartmentCommandRequest { Name = "New Department" };
        var departmentDto = new DepartmentCreateDto(request.Name);

        _mapperMock.Setup(m => m.Map<DepartmentCreateDto>(request)).Returns(departmentDto);
        _departmentService.Setup(ds => ds.CreateDepartmentAsync(departmentDto)).ReturnsAsync(false);

        // Act
        var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateDepartment_ValidDto_CallsServiceOnce()
    {
        // Arrange
        var request = new CreateDepartmentCommandRequest { Name = "New Department" };
        var departmentDto = new DepartmentCreateDto(request.Name);

        _mapperMock.Setup(m => m.Map<DepartmentCreateDto>(request)).Returns(departmentDto);
        _departmentService.Setup(ds => ds.CreateDepartmentAsync(departmentDto)).ReturnsAsync(false);

        // Act
        await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _departmentService.Verify(ds => ds.CreateDepartmentAsync(departmentDto), Times.Once);
    }
}
