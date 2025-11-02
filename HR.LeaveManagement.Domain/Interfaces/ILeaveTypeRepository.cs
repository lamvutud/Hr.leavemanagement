using HR.LeaveManagement.Domain.Models;

namespace HR.LeaveManagement.Domain.Interfaces;

public interface ILeaveTypeRepository : IRepository<LeaveType>
{
    Task<bool> IsLeaveTypeNameUniqueAsync(string name);
}
