using HR.LeaveManagement.Domain.Interfaces;
using HR.LeaveManagement.Domain.Models;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository(HrDatabaseContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    public async Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsForUser(string userId)
    {
        var leaveRequeests = await _context.LeaveRequests
            .Where(p => p.RequestingEmployeeId == userId)
            .Include(p => p.LeaveType)
            .AsNoTracking()
            .ToListAsync();

        return leaveRequeests.AsReadOnly();
    }
}
