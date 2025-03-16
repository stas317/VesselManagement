using MediatR;
using VesselManagement.Application.Commands;
using VesselManagement.Application.DTOs;
using VesselManagement.Domain.Interfaces;

namespace VesselManagement.Application.Handlers
{
	public class UpdateVesselCommandHandler : IRequestHandler<UpdateVesselCommand, VesselDto>
	{
		private readonly IVesselRepository _repository;

		public UpdateVesselCommandHandler(IVesselRepository repository)
		{
			_repository = repository;
		}

		public async Task<VesselDto> Handle(UpdateVesselCommand request, CancellationToken cancellationToken)
		{
			var vessel = await _repository.GetByIdAsync(request.Id);
			if (vessel == null)
				return null;

			vessel.Name = request.Name;
			vessel.IMO = request.IMO;
			vessel.Type = request.Type;
			vessel.Capacity = request.Capacity;

			await _repository.UpdateAsync(vessel);

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
