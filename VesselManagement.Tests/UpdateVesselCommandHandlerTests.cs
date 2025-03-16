using Microsoft.EntityFrameworkCore;
using VesselManagement.Application.Commands;
using VesselManagement.Application.DTOs;
using VesselManagement.Application.Handlers;
using VesselManagement.Infrastructure.Data;
using VesselManagement.Infrastructure.Repositories;
using Xunit;

namespace VesselManagement.Tests
{
	public class UpdateVesselCommandHandlerTests
	{
		private readonly VesselContext _context;
		private readonly VesselRepository _repository;

		public UpdateVesselCommandHandlerTests()
		{
			var options = new DbContextOptionsBuilder<VesselContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			_context = new VesselContext(options);
			_repository = new VesselRepository(_context);
		}

		[Fact]
		public async Task Handle_Should_Update_Existing_Vessel()
		{
			// Arrange
			var registerHandler = new RegisterVesselCommandHandler(_repository);
			var registerCommand = new RegisterVesselCommand("Initial Vessel", "IMO1234567", "Cargo", 5000m);
			var vesselId = await registerHandler.Handle(registerCommand, CancellationToken.None);

			// Act
			var updateHandler = new UpdateVesselCommandHandler(_repository);
			var updateCommand = new UpdateVesselCommand(vesselId, "Updated Vessel", "IMO1234567", "Cargo", 5500m);
			VesselDto updatedDto = await updateHandler.Handle(updateCommand, CancellationToken.None);

			// Assert
			Assert.NotNull(updatedDto);
			Assert.Equal("Updated Vessel", updatedDto.Name);
			Assert.Equal(5500m, updatedDto.Capacity);
		}

		[Fact]
		public async Task Handle_Should_Return_Null_For_Nonexistent_Vessel()
		{
			// Arrange
			var updateHandler = new UpdateVesselCommandHandler(_repository);
			var updateCommand = new UpdateVesselCommand(Guid.NewGuid(), "Nonexistent Vessel", "IMO0000000", "Cargo", 5000m);

			// Act
			VesselDto result = await updateHandler.Handle(updateCommand, CancellationToken.None);

			// Assert
			Assert.Null(result);
		}
	}
}
