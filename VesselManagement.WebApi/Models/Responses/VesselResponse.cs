namespace VesselManagement.WebApi.Models.Responses
{
	public class VesselResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IMO { get; set; }
		public string Type { get; set; }
		public decimal Capacity { get; set; }
	}
}
