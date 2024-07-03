using HospitalManagementSystem.Application.CQRS.Commands.Departments.DeleteDepartment;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetAllDepartments;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
using HospitalManagementSystem.Application.DTOs.Departments;

namespace HospitalManagementSystem.API.Tests.Controllers;

public class DepartmentControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private readonly DepartmentsController _departmentsController;

    public DepartmentControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _departmentsController = new DepartmentsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllDepartments_ShouldReturnDepartments()
    {
        var departmentList = new List<AllDepartmentsDto>
            {
                new AllDepartmentsDto { Id = Guid.NewGuid().ToString(), Name = "Test", DoctorsNumber = 2 },
                new AllDepartmentsDto { Id = Guid.NewGuid().ToString(), Name = "TestNew", DoctorsNumber = 1 }
            };
        var departments = new GetAllDepartmentsQueryResponse(departmentList);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllDepartmentsQueryRequest>(), default))
                        .ReturnsAsync(departments);
        var result = await _departmentsController.Get(new GetAllDepartmentsQueryRequest());
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.StatusCode.Should().Be(200);
        var returnDepartments = okResult.Value as GetAllDepartmentsQueryResponse;
        returnDepartments.Should().NotBeNull();
        returnDepartments?.Departments.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetDepartmentById_ShouldReturnDepartment()
    {
        //Arrange
        var departmentId = Guid.NewGuid();
        List<Doctor> doctorsList = new List<Doctor>
        {
            new Doctor {Id = Guid.NewGuid(),Name = "Dr. Test", Surname = "Dr. Surname", Address = "xxxx", Phone = "+994551232334", DepartmentId = Guid.NewGuid() },
        };
        var department = new GetDepartmentByIdQueryResponse(new DepartmentItemDto
        {
            Id = departmentId.ToString(),
            Name = "New Department",
            Doctors = doctorsList
        });

        var request = new GetDepartmentByIdQueryRequest
        {
            Id = departmentId.ToString(),
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByIdQueryRequest>(), default))
                         .ReturnsAsync(department);

        //Act
        var result = await _departmentsController.GetById(departmentId.ToString());

        //Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.StatusCode.Should().Be(200);
        var returnDepartment = okResult.Value as DepartmentItemDto;
        returnDepartment.Should().NotBeNull();
        returnDepartment?.Id.Should().Be(departmentId.ToString());
        returnDepartment?.Name.Should().Be("New Department");
        returnDepartment?.Doctors.Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateDepartment_ShouldReturnCreated()
    {
        //Arrange
        CreateDepartmentCommandResponse createDepartmentResponse = new CreateDepartmentCommandResponse()
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Department is successfully created"
        };
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateDepartmentCommandRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createDepartmentResponse);

        CreateDepartmentCommandRequest department = new CreateDepartmentCommandRequest()
        {
            Name = "Test-Department"
        };

        //Act
        var response = await _departmentsController.Post(department);

        //Assert
        var result = response as ObjectResult;

        //Using Assert methods
        //Assert.IsType<ObjectResult>(result);
        //Assert.Equal(result?.StatusCode, (int)HttpStatusCode.Created);

        //Using FluentAssertions
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }

    [Fact]
    public async Task UpdateDepartment_ShouldReturnOk()
    {
        UpdateDepartmentCommandResponse updateDepartment = new UpdateDepartmentCommandResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Department is successfully updated"
        };
        _mediatorMock.Setup(d => d.Send(It.IsAny<UpdateDepartmentCommandRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updateDepartment);

        UpdateDepartmentCommandRequest request = new UpdateDepartmentCommandRequest(Guid.NewGuid().ToString(), "Test_update");
        var response = await _departmentsController.Put(request);
        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteDepartment_ShouldReturnOk()
    {
        DeleteDepartmentCommandResponse deleteDepartment = new DeleteDepartmentCommandResponse 
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Department is successfully deleted"
        };
        _mediatorMock.Setup(d=>d.Send(It.IsAny<DeleteDepartmentCommandRequest>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(deleteDepartment);

        DeleteDepartmentCommandRequest request = new DeleteDepartmentCommandRequest
        {
            Id = Guid.NewGuid().ToString(),
        };

        var response = await _departmentsController.Delete(request);
        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
}