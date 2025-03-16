using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.DTOs;
using VesselManagement.Application.Handlers;
using VesselManagement.Application.Queries;
using VesselManagement.Domain.Entities;
using VesselManagement.Infrastructure.Data;
using VesselManagement.Infrastructure.Repositories;
using Xunit;

namespace VesselManagement.Tests
{
	public class GetVesselByIdQueryHandlerTests
	{
		private readonly VesselContext _context;
		private readonly VesselRepository _repository;

		public GetVesselByIdQueryHandlerTests()
		{
			var options = new DbContextOptionsBuilder<VesselContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			_context = new VesselContext(options);
			_repository = new VesselRepository(_context);
		}

		[Fact]
		public async Task Handle_Should_Return_Vessel_By_Id()
		{
			// Arrange
			var vessel = new Vessel { Name = "Test Vessel", IMO = "IMO1234567", Type = "Cargo", Capacity = 5000m };
			await _repository.AddAsync(vessel);

			var handler = new GetVesselByIdQueryHandler(_repository);
			var query = new GetVesselByIdQuery(vessel.Id);

			// Act
			VesselDto result = await handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(vessel.Name, result.Name);
		}

		[Fact]
		public async Task Handle_Should_Return_Null_For_Invalid_Id()
		{
			// Arrange
			var handler = new GetVesselByIdQueryHandler(_repository);
			var query = new GetVesselByIdQuery(Guid.NewGuid());

			// Act
			VesselDto result = await handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.Null(result);
		}
	}
}
