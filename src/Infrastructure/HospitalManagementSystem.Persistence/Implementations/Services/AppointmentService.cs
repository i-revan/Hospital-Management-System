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

    public async Task<AppointmentItemDto> GetByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return await _cacheService.GetOrCreateAsync($"{_cacheKey}_{id}", async () =>
        {
            Appointment appointment = await _unitOfWork.AppointmentReadRepository.GetByIdAsync(id, false, false, "Doctor", "User");
            if (appointment is null) throw new Exception("No associated appointment found!");
            return _mapper.Map<AppointmentItemDto>(appointment);
        });
    }

    public async Task<bool> ScheduleAppointmentAsync(ScheduleAppointmentDto dto)
    {
        var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) throw new Exception("User not found!");

        bool doesDoctorExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Id == dto.DoctorId);
        if (!doesDoctorExist) throw new Exception("Doctor not found!");
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(dto.DoctorId.ToString());

        // Check for existing appointment with the same doctor on the same day
        var isUserAppointmentWithSameDoctor = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.DoctorId == dto.DoctorId &&
                                a.User.Id == user.Id &&
                                a.StartTime.Date == dto.StartTime.Date &&
                                !a.IsDeleted);
        if (isUserAppointmentWithSameDoctor)
            throw new InvalidOperationException("You already have an appointment with this doctor on the same day.");

        // Check for overlapping appointments with different doctors
        var isUserAppointmentOverlap = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.User.Id == user.Id &&
                                ((a.StartTime >= dto.StartTime && a.StartTime < dto.EndTime) ||
                                 (a.EndTime > dto.StartTime && a.EndTime <= dto.EndTime) ||
                                 (a.StartTime <= dto.StartTime && a.EndTime >= dto.EndTime)) &&
                                !a.IsDeleted);
        if (isUserAppointmentOverlap)
            throw new InvalidOperationException("You have another appointment at the selected time.");

        var isDoctorAvailable = await _unitOfWork.AppointmentReadRepository.IsDoctorAvailableAsync(
            dto.DoctorId, dto.StartTime, dto.EndTime);
        if (!isDoctorAvailable) throw new InvalidOperationException("The doctor is not available at the selected time.");

        Appointment appointment = _mapper.Map<Appointment>(dto);
        appointment.Status = Domain.Enums.AppointmentStatus.Scheduled;
        appointment.User = user;
        bool result = await _unitOfWork.AppointmentWriteRepository.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();
        if (result) _cacheService.Remove(_cacheKey);
        return result;
    }

    public async Task<bool> UpdateAppointmentAsync(string id, AppointmentUpdateDto dto)
    {
        ArgumentNullException.ThrowIfNull(id);
        Appointment appointment = await _unitOfWork.AppointmentReadRepository.GetByIdAsync(id);
        if (appointment is null) throw new Exception("No associated appointment found!");

        var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) throw new Exception("User not found!");

        bool doesDoctorExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Id == dto.DoctorId);
        if (!doesDoctorExist) throw new Exception("Doctor not found!");
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(dto.DoctorId.ToString());

        // Check for existing appointment with the same doctor on the same day
        var isUserAppointmentWithSameDoctor = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.DoctorId == dto.DoctorId &&
                                a.User.Id == user.Id &&
                                a.StartTime.Date == dto.StartTime.Date &&
                                !a.IsDeleted);
        if (isUserAppointmentWithSameDoctor)
            throw new InvalidOperationException("You already have an appointment with this doctor on the same day.");

        // Check for overlapping appointments with different doctors
        var isUserAppointmentOverlap = await _unitOfWork.AppointmentReadRepository
            .IsExistsAsync(a => a.User.Id == user.Id &&
                                ((a.StartTime >= dto.StartTime && a.StartTime < dto.EndTime) ||
                                 (a.EndTime > dto.StartTime && a.EndTime <= dto.EndTime) ||
                                 (a.StartTime <= dto.StartTime && a.EndTime >= dto.EndTime)) &&
                                !a.IsDeleted);
        if (isUserAppointmentOverlap)
            throw new InvalidOperationException("You have another appointment at the selected time.");

        var isDoctorAvailable = await _unitOfWork.AppointmentReadRepository.IsDoctorAvailableAsync(
            dto.DoctorId, dto.StartTime, dto.EndTime);
        if (!isDoctorAvailable) throw new InvalidOperationException("The doctor is not available at the selected time.");

        _mapper.Map(dto, appointment);
        appointment.Status = Domain.Enums.AppointmentStatus.Scheduled;
        appointment.User = user;
        bool result = _unitOfWork.AppointmentWriteRepository.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;

    }

    public async Task<bool> SoftDeleteAppointmentAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Appointment appointment = await _unitOfWork.AppointmentReadRepository.GetByIdAsync(id, isTracking: true);
        if (appointment is null) throw new Exception("No appointment found!");

        var currentTime = DateTime.UtcNow;
        var timeDifference = appointment.StartTime - currentTime;

        // Check if the time difference is less than 5 hours
        if (timeDifference < TimeSpan.FromHours(5))
            throw new InvalidOperationException("Appointments cannot be canceled within 5 hours of the start time.");

        bool result = _unitOfWork.AppointmentWriteRepository.SoftDelete(appointment);
        appointment.Status = Domain.Enums.AppointmentStatus.Canceled;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;
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
