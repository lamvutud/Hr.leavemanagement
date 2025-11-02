using HR.LeaveManagement.Domain.Models;

namespace HR.LeaveManagement.Domain.Interfaces;

public interface ILeaveAllocationRepository: IRepository<LeaveAllocation>
{
    Task<IReadOnlyCollection<LeaveAllocation>> GetUserLeaveAllocations(string userId);

    Task<bool> AllocationExists(string userId, int leaveTypeId, int period);

    Task<IReadOnlyCollection<LeaveAllocation>> GetUserLeaveAllocations(string userId, int leaveTypeId);
}
