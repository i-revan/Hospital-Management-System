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
            ICollection<Department> departments = await _unitOfWork.DepartmentReadRepository.GetAllWhere().ToListAsync();
            return _mapper.Map<ICollection<DepartmentItemDto>>(departments);
        }

        public async Task<DepartmentItemDto> GetByIdAsync(int id)
        {
            if (id < 0) throw new Exception("Given id is not correct!");
            Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
            if (department is null) throw new Exception("No associated department found!");
            DepartmentItemDto dto =_mapper.Map<DepartmentItemDto>(department);
            return dto;
        }

        public async Task<bool> CreateDepartmentAsync(DepartmentCreateDto dto)
        {
            bool isExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
            if (isExist) throw new Exception("This department already exists");
            bool result = await _unitOfWork.DepartmentWriteRepository.AddAsync(_mapper.Map<Department>(dto));
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto)
        {
            if (id < 0) throw new Exception("Given id is not correct!");
            Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
            if (department is null) throw new Exception("No associated department found!");
            bool isExist = await _unitOfWork.DepartmentReadRepository.IsExistsAsync(d => d.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && !d.IsDeleted);
            if (isExist) throw new Exception("This department already exists");
            _mapper.Map(dto, department);
            bool result = _unitOfWork.DepartmentWriteRepository.Update(department);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<bool> SoftDeleteDepartmentAsync(int id)
        {
            if (id <= 0) throw new Exception("Given id is not correct!");
            Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id, isTracking: true);
            if (department is null) throw new Exception("Not Found");
            bool result = _unitOfWork.DepartmentWriteRepository.SoftDelete(department);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            if (id <= 0) throw new Exception("Given id is not correct!");
            Department department = await _unitOfWork.DepartmentReadRepository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            bool result = _unitOfWork.DepartmentWriteRepository.Delete(department);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
