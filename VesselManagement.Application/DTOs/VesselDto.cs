namespace VesselManagement.Application.DTOs
{
	public class VesselDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IMO { get; set; }
		public string Type { get; set; }
		public decimal Capacity { get; set; }
	}
}
