using AutoMapper;
using HR.LeaveManagement.Domain.Interfaces;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveTypeCommand;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly ILeaveTypeRepository repository;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = mapper.Map<Domain.Models.LeaveType>(request);

        await repository.UpdateAsync(leaveType);

        return Unit.Value;
    }
}
