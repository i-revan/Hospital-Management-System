using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.DTOs.Doctors
{
    public record DoctorUpdateDto(
        string Name,
        string Surname,
        string Address,
        string Phone,
        int DepartmentId
        );
}
