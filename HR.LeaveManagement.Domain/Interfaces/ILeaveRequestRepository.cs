using HR.LeaveManagement.Domain.Models;

namespace HR.LeaveManagement.Domain.Interfaces;

public interface ILeaveRequestRepository: IRepository<LeaveRequest>
{
    Task<IReadOnlyCollection<LeaveRequest>> GetLeaveRequestsForUser(string userId);
}
