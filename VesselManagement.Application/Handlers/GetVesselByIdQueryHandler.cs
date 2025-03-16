using MediatR;
using VesselManagement.Application.DTOs;
using VesselManagement.Application.Queries;
using VesselManagement.Domain.Interfaces;

namespace VesselManagement.Application.Handlers
{
	public class GetVesselByIdQueryHandler : IRequestHandler<GetVesselByIdQuery, VesselDto>
	{
		private readonly IVesselRepository _repository;

		public GetVesselByIdQueryHandler(IVesselRepository repository)
		{
			_repository = repository;
		}

		public async Task<VesselDto> Handle(GetVesselByIdQuery request, CancellationToken cancellationToken)
		{
			var vessel = await _repository.GetByIdAsync(request.Id);
			if (vessel == null)
				return null;

			return new VesselDto
			{
				Id = vessel.Id,
				Name = vessel.Name,
				IMO = vessel.IMO,
				Type = vessel.Type,
				Capacity = vessel.Capacity
			};
		}
	}
}
