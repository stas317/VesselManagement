using System.ComponentModel.DataAnnotations;

namespace VesselManagement.Domain.Entities
{
	public class Vessel
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public string Name { get; set; }

		[Required]
		public string IMO { get; set; }

		[Required]
		public string Type { get; set; }

		[Required]
		public decimal Capacity { get; set; }
	}
}