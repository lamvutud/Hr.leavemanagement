using System.Reflection.Metadata.Ecma335;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveTypeCommand;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveTypeCommand;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveTypeCommand;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: api/<LeaveTypesController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        var leaveTypes = await _mediator.Send<List<LeaveTypeDto>>(new GetLeaveTypesQuery());
        return Ok(leaveTypes);
    }

    // GET api/<LeaveTypesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveType = await _mediator.Send<LeaveTypeDto>(new GetLeaveTypeDetailsQuery(id));
        return leaveType == null ? NotFound() : Ok(leaveType);
    }

    // POST api/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Post([FromBody] CreateLeaveTypeCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveTypesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLeaveTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE api/<LeaveTypesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveTypeCommand() { Id = id });
        return NoContent();
    }
}
