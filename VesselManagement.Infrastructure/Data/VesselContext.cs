using Microsoft.EntityFrameworkCore;
using VesselManagement.Domain.Entities;

namespace VesselManagement.Infrastructure.Data
{
	public class VesselContext : DbContext
	{
		public VesselContext(DbContextOptions<VesselContext> options)
			: base(options) { }

		public DbSet<Vessel> Vessels { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vessel>()
				.HasIndex(v => v.IMO)
				.IsUnique();
		}
	}
}
