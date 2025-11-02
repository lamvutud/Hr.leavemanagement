using HR.LeaveManagement.Domain.Interfaces;
using HR.LeaveManagement.Domain.Models;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository(HrDatabaseContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
    public Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return _context.LeaveAllocations.AnyAsync(p =>
        p.EmployeeId == userId &&
        p.LeaveTypeId == leaveTypeId &&
        p.Period == period);
    }

    public async Task<IReadOnlyCollection<LeaveAllocation>> GetUserLeaveAllocations(string userId)
    {
        return await _context.LeaveAllocations
            .Where(x => x.EmployeeId == userId)
            .Include(x => x.LeaveType)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<LeaveAllocation>> GetUserLeaveAllocations(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
           .Where(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId)
           .Include(x => x.LeaveType)
           .AsNoTracking()
           .ToListAsync();
    }
}
