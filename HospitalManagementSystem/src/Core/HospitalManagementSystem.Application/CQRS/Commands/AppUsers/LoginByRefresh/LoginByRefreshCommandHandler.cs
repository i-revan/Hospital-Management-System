namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
public class LoginByRefreshCommandHandler : IRequestHandler<LoginByRefreshCommandRequest, 
    Result<LoginByRefreshCommandResponse>>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LoginByRefreshCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<Result<LoginByRefreshCommandResponse>> Handle(LoginByRefreshCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginByRefreshToken(request.RefreshToken);
        if (result.IsFailure) return Result<LoginByRefreshCommandResponse>.Failure(result.Error);
        var response = _mapper.Map<LoginByRefreshCommandResponse>(result.Value);
        return Result<LoginByRefreshCommandResponse>.Success(response);
    }
}