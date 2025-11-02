using FluentValidation;
using HR.LeaveManagement.Domain.Interfaces;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveTypeCommand;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository repository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        RuleFor(p => p.Name)
            .NotNull().NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} should not exceed 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} can not exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} can not less than 1");

        RuleFor(p => p)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave Type already exists");
        this.repository = repository;
    }

    private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        var result = await repository.IsLeaveTypeNameUniqueAsync(command.Name);
        return result;
    }
}
