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

    public async Task<DoctorItemDto> GetByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return await _cacheService.GetOrCreateAsync($"{_cacheKey}_{id}", async () =>
        {
            Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id, includes: "Department");
            if (doctor is null) throw new Exception("No associated doctor found!");
            return _mapper.Map<DoctorItemDto>(doctor);
        });
    }

    public async Task<bool> CreateDoctorAsync(DoctorCreateDto dto)
    {
        bool isExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) throw new Exception("This doctor already exists");
        bool isDepartmentExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Id == dto.DepartmentId);
        if (!isDepartmentExist) throw new Exception("Selected department does not exist!");
        bool result = await _unitOfWork.DoctorWriteRepository.AddAsync(_mapper.Map<Doctor>(dto));
        await _unitOfWork.SaveChangesAsync();
        if (result) _cacheService.Remove(_cacheKey);
        return result;
    }

    public async Task<bool> UpdateDoctorAsync(string id, DoctorUpdateDto dto)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id);
        if (doctor is null) throw new Exception("No associated doctor found!");
        bool isExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) throw new Exception("This doctor already exists");
        _mapper.Map(dto, doctor);
        bool result = _unitOfWork.DoctorWriteRepository.Update(doctor);
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;
    }

    public async Task<bool> SoftDeleteDoctorAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id, isTracking: true);
        if (doctor is null) throw new Exception("No associated doctor found!");
        bool result = _unitOfWork.DoctorWriteRepository.SoftDelete(doctor);
        await _unitOfWork.SaveChangesAsync();
        _clearCache(id, result);
        return result;
    }

    public async Task<bool> DeleteDoctorAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id);
        if (doctor is null) throw new Exception("No associated doctor found!");
        bool result = _unitOfWork.DoctorWriteRepository.Delete(doctor);
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
