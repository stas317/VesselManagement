using System.ComponentModel.DataAnnotations;

namespace VesselManagement.WebApi.Models.Requests
{
	public class RegisterVesselRequest
	{
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
