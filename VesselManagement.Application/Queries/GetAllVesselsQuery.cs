using MediatR;
using VesselManagement.Application.DTOs;

namespace VesselManagement.Application.Queries
{
	public record GetAllVesselsQuery() : IRequest<IEnumerable<VesselDto>>;
}
