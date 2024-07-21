using HospitalManagementSystem.Application.Common.Errors;
using HospitalManagementSystem.Application.DTOs.Doctors;

namespace HospitalManagementSystem.Persistence.Implementations.Services;
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly string _cacheKey = "doctors";

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<ICollection<DoctorItemDto>> GetAllAsync()
    {
        return await _cacheService.GetOrCreateAsync(_cacheKey, async () =>
        {
            var doctors = await _unitOfWork.DoctorReadRepository.GetAll(includes: "Department").ToListAsync();
            return _mapper.Map<ICollection<DoctorItemDto>>(doctors);
        });
    }

    public async Task<Result<DoctorItemDto>> GetByIdAsync(string id)
    {
        if (id is null) return Result<DoctorItemDto>.Failure(CommonErrors.InvalidId);
        return await _cacheService.GetOrCreateAsync($"{_cacheKey}_{id}", async () =>
        {
            Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id, includes: "Department");
            if (doctor is null) return DoctorErrors.DoctorNotFound;
            return Result<DoctorItemDto>.Success(_mapper.Map<DoctorItemDto>(doctor));
        });
    }

    public async Task<Result<bool>> CreateDoctorAsync(DoctorCreateDto dto)
    {
        //bool isExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        //if (isExist) return Result<bool>.Failure(DoctorErrors.DoctorAlreadyExist);
        bool isDepartmentExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Id == dto.DepartmentId);
        if (!isDepartmentExist) return DoctorErrors.DoctorDepartmentDoesNotExist;
        bool result = await _unitOfWork.DoctorWriteRepository.AddAsync(_mapper.Map<Doctor>(dto));
        if (!result) return DoctorErrors.DoctorCreationFailed;
        await _unitOfWork.SaveChangesAsync();
        _cacheService.Remove(_cacheKey);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UpdateDoctorAsync(string id, DoctorUpdateDto dto)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id);
        if (doctor is null) return DoctorErrors.DoctorNotFound;
        //bool isExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        //if (isExist) return Result<bool>.Failure(DoctorErrors.DoctorAlreadyExist);
        bool isDepartmentExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Id == dto.DepartmentId);
        if (!isDepartmentExist) return DoctorErrors.DoctorDepartmentDoesNotExist;
        _mapper.Map(dto, doctor);
        bool result = _unitOfWork.DoctorWriteRepository.Update(doctor);
        if (!result) return DoctorErrors.DoctorUpdatingFailed;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> SoftDeleteDoctorAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id, isTracking: true);
        if (doctor is null) return DoctorErrors.DoctorNotFound;
        bool result = _unitOfWork.DoctorWriteRepository.SoftDelete(doctor);
        if (!result) return DoctorErrors.DoctorDeletingFailed;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteDoctorAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id);
        if (doctor is null) return DoctorErrors.DoctorNotFound;
        bool result = _unitOfWork.DoctorWriteRepository.Delete(doctor);
        if (!result) return DoctorErrors.DoctorDeletingFailed;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(true);
    }

    private void _clearCache(string id, bool result)
    {
        if (result)
        {
            _cacheService.Remove(_cacheKey); // Invalidate cache
            _cacheService.Remove($"{_cacheKey}_{id}"); // Invalidate individual cache entry
        }
    }
}
