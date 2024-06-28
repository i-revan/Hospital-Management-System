using AutoMapper;
using HospitalManagementSystem.Application;
using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.DTOs.Departments;
using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Persistence.Implementations.Services
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<DepartmentItemDto>> GetAllAsync()
        {
            ICollection<Department> departments = await _unitOfWork.DepartmentRepository.GetAllWhere().ToListAsync();
            return _mapper.Map<ICollection<DepartmentItemDto>>(departments);
        }

        public async Task<DepartmentItemDto> GetByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentException("No associated department found!");
            Department department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is null) throw new ArgumentException("No associated department found!");
            DepartmentItemDto dto =_mapper.Map<DepartmentItemDto>(department);
            return dto;
        }

        public async Task CreateAsync(DepartmentCreateDto dto)
        {
            bool isExist = await _unitOfWork.DepartmentRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
            if (isExist) throw new Exception("This department already exists");
            await _unitOfWork.DepartmentRepository.AddAsync(_mapper.Map<Department>(dto));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task PutAsync(int id, DepartmentUpdateDto dto)
        {
            Department department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is null) throw new ArgumentException("No associated department found!");
            bool isExist = await _unitOfWork.DepartmentRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
            if (isExist) throw new Exception("This department already exists");
            _mapper.Map(dto, department);
            _unitOfWork.DepartmentRepository.Update(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Department department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id, isTracking: true);
            if (department is null) throw new Exception("Not Found");
            _unitOfWork.DepartmentRepository.SoftDelete(department);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Department department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            _unitOfWork.DepartmentRepository.Delete(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
