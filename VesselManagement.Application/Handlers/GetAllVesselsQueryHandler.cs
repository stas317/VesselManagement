using MediatR;
using VesselManagement.Application.DTOs;
using VesselManagement.Application.Queries;
using VesselManagement.Domain.Interfaces;

namespace VesselManagement.Application.Handlers
{
	public class GetAllVesselsQueryHandler : IRequestHandler<GetAllVesselsQuery, IEnumerable<VesselDto>>
	{
		private readonly IVesselRepository _repository;

		public GetAllVesselsQueryHandler(IVesselRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<VesselDto>> Handle(GetAllVesselsQuery request, CancellationToken cancellationToken)
		{
			var vessels = await _repository.GetAllAsync();
			return vessels.Select(v => new VesselDto
			{
				Id = v.Id,
				Name = v.Name,
				IMO = v.IMO,
				Type = v.Type,
				Capacity = v.Capacity
			});
		}
	}
}
