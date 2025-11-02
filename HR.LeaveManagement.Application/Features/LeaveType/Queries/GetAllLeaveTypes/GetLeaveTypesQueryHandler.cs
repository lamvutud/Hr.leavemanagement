using AutoMapper;
using HR.LeaveManagement.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(ILeaveTypeRepository repository, IMapper mapper, ILogger<GetLeaveTypesQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        this._logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query data from database
        var leaveTypes = await _repository.GetAllAsync();

        // Convert entities to Dtos
        var dtos = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        _logger.LogInformation("Retrieved list of leave types");
        // Return Dtos
        return dtos;
    }
}
