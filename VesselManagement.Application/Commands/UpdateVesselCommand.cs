using MediatR;
using VesselManagement.Application.DTOs;

namespace VesselManagement.Application.Commands
{
	public record UpdateVesselCommand(
		Guid Id,
		string Name,
		string IMO,
		string Type,
		decimal Capacity) : IRequest<VesselDto>;
}
