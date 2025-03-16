using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Commands;
using VesselManagement.Application.Handlers;
using VesselManagement.Infrastructure.Data;
using VesselManagement.Infrastructure.Repositories;
using Xunit;

namespace VesselManagement.Tests
{
	public class RegisterVesselCommandHandlerTests
	{
		private readonly VesselContext _context;
		private readonly VesselRepository _repository;

		public RegisterVesselCommandHandlerTests()
		{
			var options = new DbContextOptionsBuilder<VesselContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			_context = new VesselContext(options);
			_repository = new VesselRepository(_context);
		}

		[Fact]
		public async Task Handle_Should_Register_Vessel_And_Return_Id()
		{
			// Arrange
			var handler = new RegisterVesselCommandHandler(_repository);
			var command = new RegisterVesselCommand("Test Vessel", "IMO1234567", "Cargo", 5000m);

			// Act
			var vesselId = await handler.Handle(command, CancellationToken.None);

			// Assert
			var vessel = await _repository.GetByIdAsync(vesselId);
			Assert.NotNull(vessel);
			Assert.Equal("Test Vessel", vessel.Name);
		}

		[Fact]
		public async Task Handle_Should_Throw_Exception_For_Duplicate_IMO()
		{
			// Arrange
			var handler = new RegisterVesselCommandHandler(_repository);
			var command1 = new RegisterVesselCommand("Vessel 1", "IMO1234567", "Cargo", 5000m);
			var command2 = new RegisterVesselCommand("Vessel 2", "IMO1234567", "Tanker", 6000m);

			// Act
			await handler.Handle(command1, CancellationToken.None);

			// Assert
			await Assert.ThrowsAsync<Exception>(() => handler.Handle(command2, CancellationToken.None));
		}
	}
}
