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

    public async Task<DepartmentItemDto> GetByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return await _cacheService.GetOrCreateAsync($"{_cacheKey}_{id}", async () =>
        {
            Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id, includes: "Doctors");
            if (department is null) throw new Exception("No associated department found!");
            return _mapper.Map<DepartmentItemDto>(department);
        });
    }

    public async Task<bool> CreateDepartmentAsync(DepartmentCreateDto dto)
    {
        bool isExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) throw new Exception("This department already exists");
        bool result = await _unitOfWork.DepartmentWriteRepository.AddAsync(_mapper.Map<Department>(dto));
        await _unitOfWork.SaveChangesAsync();
        if (result)  _cacheService.Remove(_cacheKey);
        return result;
    }

    public async Task<bool> UpdateDepartmentAsync(string id, DepartmentUpdateDto dto)
    {
        ArgumentNullException.ThrowIfNull(id);
        Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
        if (department is null) throw new Exception("No associated department found!");
        bool isExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) throw new Exception("This department already exists");
        _mapper.Map(dto, department);
        bool result = _unitOfWork.DepartmentWriteRepository.Update(department);
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;
    }

    public async Task<bool> SoftDeleteDepartmentAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id, isTracking: true);
        if (department is null) throw new Exception("Not Found");
        bool result = _unitOfWork.DepartmentWriteRepository.SoftDelete(department);
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;
    }

    public async Task<bool> DeleteDepartmentAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
        if (department is null) throw new Exception("Not found");
        bool result = _unitOfWork.DepartmentWriteRepository.Delete(department);
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;
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
