namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, Result<LoginUserCommandResponse>>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        LoginDto dto = _mapper.Map<LoginDto>(request);
        var result = await _authService.Login(dto);
        if (result.IsFailure) return Result<LoginUserCommandResponse>.Failure(result.Error);
        var response = _mapper.Map<LoginUserCommandResponse>(result.Value);
        return Result<LoginUserCommandResponse>.Success(response);
    }
}