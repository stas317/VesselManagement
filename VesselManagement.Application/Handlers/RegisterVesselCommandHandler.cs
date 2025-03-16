using MediatR;
using VesselManagement.Application.Commands;
using VesselManagement.Domain.Entities;
using VesselManagement.Domain.Interfaces;

namespace VesselManagement.Application.Handlers
{
	public class RegisterVesselCommandHandler : IRequestHandler<RegisterVesselCommand, Guid>
	{
		private readonly IVesselRepository _repository;

		public RegisterVesselCommandHandler(IVesselRepository repository)
		{
			_repository = repository;
		}

		public async Task<Guid> Handle(RegisterVesselCommand request, CancellationToken cancellationToken)
		{
			if (!await _repository.IsImoUniqueAsync(request.IMO))
			{
				throw new Exception("Vessel with the same IMO already exists.");
			}

			var vessel = new Vessel
			{
				Name = request.Name,
				IMO = request.IMO,
				Type = request.Type,
				Capacity = request.Capacity
			};

			await _repository.AddAsync(vessel);
			return vessel.Id;
		}
	}
}
