using Microsoft.EntityFrameworkCore;
using VesselManagement.Domain.Entities;
using VesselManagement.Domain.Interfaces;
using VesselManagement.Infrastructure.Data;

namespace VesselManagement.Infrastructure.Repositories
{
	public class VesselRepository : IVesselRepository
	{
		private readonly VesselContext _context;

		public VesselRepository(VesselContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Vessel vessel)
		{
			_context.Vessels.Add(vessel);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Vessel>> GetAllAsync()
		{
			var result = await _context.Vessels.ToListAsync();
			return result;
		}

		public async Task<Vessel> GetByIdAsync(Guid id)
		{
			var result = await _context.Vessels.FindAsync(id);
			return result;
		}

		public async Task UpdateAsync(Vessel vessel)
		{
			_context.Vessels.Update(vessel);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> IsImoUniqueAsync(string imo, Guid? vesselId = null)
		{
			var vessel = await _context.Vessels.FirstOrDefaultAsync(v => v.IMO == imo);
			if (vessel == null || (vesselId.HasValue && vessel.Id == vesselId.Value))
				return true;

			return false;
		}
	}
}
