using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain.Interfaces;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveTypeCommand;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository repository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await repository.GetByIdAync(request.Id);

        if (leaveType == null)
        {
            throw new NotFoundException($"{nameof(LeaveType)} can not be found with {request.Id}");
        }

        await repository.DeleteAsync(leaveType);

        return Unit.Value;
    }
}
