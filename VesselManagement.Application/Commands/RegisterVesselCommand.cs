using MediatR;

namespace VesselManagement.Application.Commands
{
	public record RegisterVesselCommand(
		string Name,
		string IMO,
		string Type,
		decimal Capacity) : IRequest<Guid>;
}