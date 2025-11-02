using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain.Interfaces;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveTypeCommand;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository repository;
    private readonly IMapper mapper;
    private readonly IValidator<CreateLeaveTypeCommand> validator;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper, IValidator<CreateLeaveTypeCommand> validator)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validation
        //var validator = new CreateLeaveTypeCommandValidator(repository);
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException($"Invalid {nameof(LeaveType)}", validationResult);
        }

        // Convert to domain entity
        var leaveType = mapper.Map<Domain.Models.LeaveType>(request);

        // add to database
        await repository.CreateAsync(leaveType);

        // return record id
        return leaveType.Id;
    }
}
