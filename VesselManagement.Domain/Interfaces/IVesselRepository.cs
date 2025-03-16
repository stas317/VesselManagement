using VesselManagement.Domain.Entities;

namespace VesselManagement.Domain.Interfaces
{
	public interface IVesselRepository
	{
		Task AddAsync(Vessel vessel);
		Task<Vessel> GetByIdAsync(Guid id);
		Task<IEnumerable<Vessel>> GetAllAsync();
		Task UpdateAsync(Vessel vessel);
		Task<bool> IsImoUniqueAsync(string imo, Guid? vesselId = null);
	}
}
