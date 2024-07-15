
namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IAuthService authService,IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        RegisterDto dto = _mapper.Map<RegisterDto>(request);
        var result = await _authService.Register(dto);
        return new()
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? result.Message : "Something went wrong"
        };
    }
}
