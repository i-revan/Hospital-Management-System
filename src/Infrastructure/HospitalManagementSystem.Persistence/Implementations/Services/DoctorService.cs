using HospitalManagementSystem.Application.Abstraction.Repositories;
using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.DTOs.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Persistence.Implementations.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }
        public Task CreateAsync(DoctorCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DoctorItemDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DoctorItemDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(int id, DoctorUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
