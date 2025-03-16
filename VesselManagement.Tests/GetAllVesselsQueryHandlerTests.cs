using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Handlers;
using VesselManagement.Application.Queries;
using VesselManagement.Domain.Entities;
using VesselManagement.Infrastructure.Data;
using VesselManagement.Infrastructure.Repositories;
using Xunit;

namespace VesselManagement.Tests
{
	public class GetAllVesselsQueryHandlerTests
	{
		private readonly VesselContext _context;
		private readonly VesselRepository _repository;

		public GetAllVesselsQueryHandlerTests()
		{
			var options = new DbContextOptionsBuilder<VesselContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			_context = new VesselContext(options);
			_repository = new VesselRepository(_context);
		}

		[Fact]
		public async Task Handle_Should_Return_All_Vessels()
		{
			// Arrange
			var vessel1 = new Vessel { Name = "Vessel 1", IMO = "IMO1", Type = "Cargo", Capacity = 5000m };
			var vessel2 = new Vessel { Name = "Vessel 2", IMO = "IMO2", Type = "Tanker", Capacity = 6000m };
			await _repository.AddAsync(vessel1);
			await _repository.AddAsync(vessel2);

			var handler = new GetAllVesselsQueryHandler(_repository);
			var query = new GetAllVesselsQuery();

			// Act
			var result = await handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
		}
	}
}
