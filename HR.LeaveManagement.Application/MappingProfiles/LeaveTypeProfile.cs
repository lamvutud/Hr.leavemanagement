using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveTypeCommand;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveTypeCommand;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Domain.Models;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}
