using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Domain.Interfaces;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository repository) : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDto>
{
    private readonly IMapper mapper = mapper;
    private readonly ILeaveTypeRepository repository = repository;

    public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await repository.GetByIdAync(request.Id);

        if (leaveType == null)
        {
            throw new NotFoundException($"{nameof(LeaveType)} can not be found with id {request.Id}");
        }

        var dto = mapper.Map<LeaveTypeDto>(leaveType);

        return dto;
    }
}
