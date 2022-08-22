using System;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.Services
{
    public interface IStaffService
    {
        Task<List<StaffModel>> GetStaffList();
        Task<int> AddStaff(StaffModel staffModel);
        Task<int> DeleteStaff(StaffModel staffModel);
        Task<int> UpdateStaff(StaffModel staffModel);
    }
}

