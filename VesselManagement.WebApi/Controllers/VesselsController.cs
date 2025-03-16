using MediatR;
using Microsoft.AspNetCore.Mvc;
using VesselManagement.Application.Commands;
using VesselManagement.Application.Queries;
using VesselManagement.WebApi.Models.Requests;
using VesselManagement.WebApi.Models.Responses;

namespace VesselManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VesselsController : ControllerBase
{
	private readonly IMediator _mediator;

	public VesselsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> RegisterVessel([FromBody] RegisterVesselRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var command = new RegisterVesselCommand(
			request.Name,
			request.IMO,
			request.Type,
			request.Capacity);

		try
		{
			var vesselId = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetVesselById), new { id = vesselId }, new { Id = vesselId });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = ex.Message });
		}
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateVessel(Guid id, [FromBody] UpdateVesselRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var command = new UpdateVesselCommand(
			id,
			request.Name,
			request.IMO,
			request.Type,
			request.Capacity);

		var updatedVessel = await _mediator.Send(command);
		if (updatedVessel == null)
			return NotFound(new { message = "Vessel not found or update failed" });

		var response = new VesselResponse
		{
			Id = updatedVessel.Id,
			Name = updatedVessel.Name,
			IMO = updatedVessel.IMO,
			Type = updatedVessel.Type,
			Capacity = updatedVessel.Capacity
		};

		return Ok(response);
	}

	[HttpGet]
	public async Task<IActionResult> GetAllVessels()
	{
		var vesselDtos = await _mediator.Send(new GetAllVesselsQuery());

		var responses = vesselDtos.Select(dto => new VesselResponse
		{
			Id = dto.Id,
			Name = dto.Name,
			IMO = dto.IMO,
			Type = dto.Type,
			Capacity = dto.Capacity
		});

		return Ok(responses);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetVesselById(Guid id)
	{
		var vesselDto = await _mediator.Send(new GetVesselByIdQuery(id));
		if (vesselDto == null)
			return NotFound(new { message = "Vessel not found" });

		var response = new VesselResponse
		{
			Id = vesselDto.Id,
			Name = vesselDto.Name,
			IMO = vesselDto.IMO,
			Type = vesselDto.Type,
			Capacity = vesselDto.Capacity
		};

		return Ok(response);
	}
}