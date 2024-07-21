using HospitalManagementSystem.Application.Common.Errors;

namespace HospitalManagementSystem.Persistence.Implementations.Services;

internal class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly string _cacheKey = "departments";

    public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<ICollection<AllDepartmentsDto>> GetAllAsync()
    {
        return await _cacheService.GetOrCreateAsync(_cacheKey, async () =>
        {
            var departments = await _unitOfWork.DepartmentReadRepository.GetAll(includes: "Doctors").ToListAsync();
            return _mapper.Map<ICollection<AllDepartmentsDto>>(departments);
        });

    }

    public async Task<Result<DepartmentItemDto>> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return Result<DepartmentItemDto>.Failure(CommonErrors.InvalidId);
        return await _cacheService.GetOrCreateAsync($"{_cacheKey}_{id}", async () =>
        {
            Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id, includes: "Doctors");
            if (department is null) return DepartmentErrors.DepartmentNotFound;
            DepartmentItemDto departmentDto = _mapper.Map<DepartmentItemDto>(department);
            return Result<DepartmentItemDto>.Success(departmentDto);
        });
    }

    public async Task<Result<bool>> CreateDepartmentAsync(DepartmentCreateDto dto)
    {
        bool isExist = await _unitOfWork.DepartmentReadRepository
            .IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) return DepartmentErrors.DepartmentAlreadyExist;
        bool result = await _unitOfWork.DepartmentWriteRepository.AddAsync(_mapper.Map<Department>(dto));
        if (!result) return DepartmentErrors.DepartmentCreationFailed;
        await _unitOfWork.SaveChangesAsync();
        _cacheService.Remove(_cacheKey);
        return Result<bool>.Success(result);
    }

    public async Task<Result<bool>> UpdateDepartmentAsync(string id, DepartmentUpdateDto dto)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
        if (department is null) Result<bool>.Failure(DepartmentErrors.DepartmentNotFound);
        bool isExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) return DepartmentErrors.DepartmentAlreadyExist;
        _mapper.Map(dto, department);
        bool result = _unitOfWork.DepartmentWriteRepository.Update(department);
        if(!result) return DepartmentErrors.DepartmentUpdatingFailed;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(result);
    }

    public async Task<Result<bool>> SoftDeleteDepartmentAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id, isTracking: true);
        if (department is null) return DepartmentErrors.DepartmentNotFound;
        bool result = _unitOfWork.DepartmentWriteRepository.SoftDelete(department);
        if (!result) return DepartmentErrors.DepartmentDeletingFailed;
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteDepartmentAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return CommonErrors.InvalidId;
        Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
        if (department is null) return DepartmentErrors.DepartmentNotFound;
        bool result = _unitOfWork.DepartmentWriteRepository.Delete(department);
        if (!result) return DepartmentErrors.DepartmentDeletingFailed;
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
