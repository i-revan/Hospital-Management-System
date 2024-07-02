using HospitalManagementSystem.Application;
using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.DTOs.Doctors;

namespace HospitalManagementSystem.Persistence.Implementations.Services;
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<DoctorItemDto>> GetAllAsync()
    {
        ICollection<Doctor> doctors = await _unitOfWork.DoctorReadRepository.GetAll(includes: "Department").ToListAsync();
        return _mapper.Map<ICollection<DoctorItemDto>>(doctors);
    }

    public async Task<DoctorItemDto> GetByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id,includes: "Department");
        if (doctor is null) throw new Exception("No associated doctor found!");
        return _mapper.Map<DoctorItemDto>(doctor);
    }

    public async Task<bool> CreateDoctorAsync(DoctorCreateDto dto)
    {
        bool isExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) throw new Exception("This doctor already exists");
        bool isDepartmentExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Id == dto.DepartmentId);
        if (!isDepartmentExist) throw new Exception("Selected department does not exist!");
        bool result = await _unitOfWork.DoctorWriteRepository.AddAsync(_mapper.Map<Doctor>(dto));
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<bool> UpdateDoctorAsync(string id, DoctorUpdateDto dto)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id);
        if (doctor is null) throw new Exception("No associated doctor found!");
        bool isExist = await _unitOfWork.DoctorReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
        if (isExist) throw new Exception("This department already exists");
        _mapper.Map(dto, doctor);
        bool result = _unitOfWork.DoctorWriteRepository.Update(doctor);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<bool> SoftDeleteDoctorAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id, isTracking: true);
        if (doctor is null) throw new Exception("No associated doctor found!");
        bool result = _unitOfWork.DoctorWriteRepository.SoftDelete(doctor);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeleteDoctorAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Doctor doctor = await _unitOfWork.DoctorReadRepository.GetByIdAsync(id);
        if (doctor is null) throw new Exception("No associated doctor found!");
        bool result = _unitOfWork.DoctorWriteRepository.Delete(doctor);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }
}
