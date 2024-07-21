using HospitalManagementSystem.Application.Common.Errors;
using HospitalManagementSystem.Application.DTOs.Appointments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace HospitalManagementSystem.Persistence.Implementations.Services;
public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly string _cacheKey = "appointments";

    public AppointmentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager,IMapper mapper,ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<ICollection<GetAllAppointmentsDto>> GetAllAsync()
    {
        return await _cacheService.GetOrCreateAsync(_cacheKey, async () =>
        {
            var appointments = await _unitOfWork.AppointmentReadRepository
            .GetAll(false,false, "Doctor", "User")
            .ToListAsync();
            return _mapper.Map<ICollection<GetAllAppointmentsDto>>(appointments);
        });
    }

    public async Task<Result<AppointmentItemDto>> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        return await _cacheService.GetOrCreateAsync($"{_cacheKey}_{id}", async () =>
        {
            Appointment appointment = await _unitOfWork.AppointmentReadRepository.GetByIdAsync(id, false, false, "Doctor", "User");
            if (appointment is null) return AppointmentErrors.AppointmentNotFound;
            return Result<AppointmentItemDto>.Success(_mapper.Map<AppointmentItemDto>(appointment));
        });
    }

    public async Task<Result<bool>> ScheduleAppointmentAsync(ScheduleAppointmentDto dto)
    {
        var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return UserErrors.UserNotFound;

        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(dto.DoctorId.ToString());
        if (doctor is null) return DoctorErrors.DoctorNotFound;

        // Check for existing appointment with the same doctor on the same day
        var isUserAppointmentWithSameDoctor = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.DoctorId == dto.DoctorId &&
                                a.User.Id == user.Id &&
                                a.StartTime.Date == dto.StartTime.Date &&
                                !a.IsDeleted);
        if (isUserAppointmentWithSameDoctor) return AppointmentErrors.AppointmentWithSameDoctor;

        // Check for overlapping appointments with different doctors
        var isUserAppointmentOverlap = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.User.Id == user.Id &&
                                ((a.StartTime >= dto.StartTime && a.StartTime < dto.EndTime) ||
                                 (a.EndTime > dto.StartTime && a.EndTime <= dto.EndTime) ||
                                 (a.StartTime <= dto.StartTime && a.EndTime >= dto.EndTime)) &&
                                !a.IsDeleted);
        if (isUserAppointmentOverlap) return AppointmentErrors.AppointmentOverlap;

        var isDoctorAvailable = await _unitOfWork.AppointmentReadRepository.IsDoctorAvailableAsync(
            dto.DoctorId, dto.StartTime, dto.EndTime);
        if (!isDoctorAvailable) return DoctorErrors.DoctorNotAvailable;

        Appointment appointment = _mapper.Map<Appointment>(dto);
        appointment.Status = Domain.Enums.AppointmentStatus.Scheduled;
        appointment.User = user;
        bool result = await _unitOfWork.AppointmentWriteRepository.AddAsync(appointment);
        if (!result) return AppointmentErrors.AppointmentScheduleFailed;
        await _unitOfWork.SaveChangesAsync();
        if (result) _cacheService.Remove(_cacheKey);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UpdateAppointmentAsync(string id, AppointmentUpdateDto dto)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Appointment appointment = await _unitOfWork.AppointmentReadRepository.GetByIdAsync(id);
        if (appointment is null) return AppointmentErrors.AppointmentNotFound;

        var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) return UserErrors.UserNotFound;

        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(dto.DoctorId.ToString());
        if(doctor is null) return DoctorErrors.DoctorNotFound;

        // Check for existing appointment with the same doctor on the same day
        var isUserAppointmentWithSameDoctor = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.DoctorId == dto.DoctorId &&
                                a.User.Id == user.Id &&
                                a.StartTime.Date == dto.StartTime.Date &&
                                !a.IsDeleted);
        if (isUserAppointmentWithSameDoctor) return AppointmentErrors.AppointmentWithSameDoctor;

        // Check for overlapping appointments with different doctors
        var isUserAppointmentOverlap = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.User.Id == user.Id &&
                                ((a.StartTime >= dto.StartTime && a.StartTime < dto.EndTime) ||
                                 (a.EndTime > dto.StartTime && a.EndTime <= dto.EndTime) ||
                                 (a.StartTime <= dto.StartTime && a.EndTime >= dto.EndTime)) &&
                                !a.IsDeleted);
        if (isUserAppointmentOverlap) return AppointmentErrors.AppointmentOverlap;

        var isDoctorAvailable = await _unitOfWork.AppointmentReadRepository.IsDoctorAvailableAsync(
            dto.DoctorId, dto.StartTime, dto.EndTime);
        if (!isDoctorAvailable) return DoctorErrors.DoctorNotAvailable;

        _mapper.Map(dto, appointment);
        appointment.Status = Domain.Enums.AppointmentStatus.Scheduled;
        appointment.User = user;
        bool result = _unitOfWork.AppointmentWriteRepository.Update(appointment);
        if (!result) return AppointmentErrors.AppointmentUpdatingFailed;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(true);

    }

    public async Task<Result<bool>> SoftDeleteAppointmentAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Appointment appointment = await _unitOfWork.AppointmentReadRepository.GetByIdAsync(id, isTracking: true);
        if (appointment is null) return AppointmentErrors.AppointmentNotFound;

        var currentTime = DateTime.UtcNow;
        var timeDifference = appointment.StartTime - currentTime;

        // Check if the time difference is less than 5 hours
        if (timeDifference < TimeSpan.FromHours(5)) return AppointmentErrors.AppointmentCancel;

        bool result = _unitOfWork.AppointmentWriteRepository.SoftDelete(appointment);
        if (!result) return AppointmentErrors.AppointmentDeletingFailed;
        appointment.Status = Domain.Enums.AppointmentStatus.Canceled;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(true);
    }

    private void _clearCache(string id, bool result)
    {
        if (result)
        {
            _cacheService.Remove(_cacheKey);
            _cacheService.Remove($"{_cacheKey}_{id}");
        }
    }
}
