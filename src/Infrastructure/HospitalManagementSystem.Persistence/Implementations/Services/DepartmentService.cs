using AutoMapper;
using HospitalManagementSystem.Application.Abstraction.Repositories;
using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.DTOs.Departments;
using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Persistence.Implementations.Services
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<DepartmentItemDto>> GetAllAsync()
        {
            ICollection<Department> departments = await _repository.GetAllWhere().ToListAsync();
            return _mapper.Map<ICollection<DepartmentItemDto>>(departments);
        }

        public async Task CreateAsync(DepartmentCreateDto dto)
        {
            await _repository.AddAsync(_mapper.Map<Department>(dto));
            await _repository.SaveChangeAsync();
        }


        public async Task SoftDeleteAsync(int id)
        {
            Department department = await _repository.GetByIdAsync(id,isTracking: true);
            if (department is null) throw new Exception("Not Found");
            _repository.SoftDelete(department);
            await _repository.SaveChangeAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Department department = await _repository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            _repository.Delete(department);
            await _repository.SaveChangeAsync();
        }
    }
}
