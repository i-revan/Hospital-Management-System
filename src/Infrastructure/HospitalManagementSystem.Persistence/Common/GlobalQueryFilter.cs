using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<T>(this ModelBuilder modelBuilder) where T : BaseEntity, new()
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => e.IsDeleted == false);
        }
        public static void ApplyFilters(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyFilter<Department>();
            modelBuilder.ApplyFilter<Doctor>();
        }
    }
}
