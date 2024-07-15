namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
public class LoginByRefreshCommandHandler : IRequestHandler<LoginByRefreshCommandRequest, LoginByRefreshCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LoginByRefreshCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<LoginByRefreshCommandResponse> Handle(LoginByRefreshCommandRequest request, CancellationToken cancellationToken)
    {
        TokenResponseDto tokenResponseDto = await _authService.LoginByRefreshToken(request.RefreshToken);
        var response = _mapper.Map<LoginByRefreshCommandResponse>(tokenResponseDto);
        return response;
    }
}