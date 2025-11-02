using HR.LeaveManagement.Domain.Interfaces;
using HR.LeaveManagement.Domain.Models;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository(HrDatabaseContext context) : GenericRepository<LeaveType>(context), ILeaveTypeRepository
{
    public async Task<bool> IsLeaveTypeNameUniqueAsync(string name)
    {
        return await _context.LeaveTypes.AnyAsync(p => p.Name == name) == false;
    }
}
