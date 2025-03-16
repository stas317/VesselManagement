using MediatR;
using VesselManagement.Application.DTOs;

namespace VesselManagement.Application.Queries
{
	public record GetVesselByIdQuery(Guid Id) : IRequest<VesselDto>;
}
