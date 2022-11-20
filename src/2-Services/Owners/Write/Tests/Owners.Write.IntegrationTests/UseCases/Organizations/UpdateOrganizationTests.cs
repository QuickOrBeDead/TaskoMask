﻿using FluentAssertions;
using TaskoMask.BuildingBlocks.Contracts.Resources;
using TaskoMask.Services.Owners.Write.Application.UseCases.Organizations.UpdateOrganization;
using TaskoMask.Services.Owners.Write.IntegrationTests.Fixtures;
using TaskoMask.Services.Owners.Write.IntegrationTests.TestData;
using Xunit;

namespace TaskoMask.Services.Owners.Write.IntegrationTests.UseCases.Organizations
{
    [Collection(nameof(OrganizationCollectionFixture))]
    public class UpdateOrganizationTests
    {

        #region Fields

        private readonly OrganizationCollectionFixture _fixture;

        #endregion

        #region Ctor

        public UpdateOrganizationTests(OrganizationCollectionFixture fixture)
        {
            _fixture = fixture;
        }

        #endregion

        #region Test Methods


        [Fact]
        public async Task Organization_Is_Updated_Properly()
        {
            //Arrange
            var expectedOwner = OwnerObjectMother.GetAnOwnerWithAnOrganization(_fixture.OwnerValidatorService);
            var expectedOrganization = expectedOwner.Organizations.FirstOrDefault();
            await _fixture.SeedOwnerAsync(expectedOwner);

            var request = new UpdateOrganizationRequest(id: expectedOrganization.Id, name: "Test New Name", description: "Test New Description");
            var updateOrganizationUseCase = new UpdateOrganizationUseCase(_fixture.OwnerAggregateRepository, _fixture.MessageBus, _fixture.InMemoryBus);

            //Act
            var result = await updateOrganizationUseCase.Handle(request, CancellationToken.None);

            //Assert
            result.EntityId.Should().Be(expectedOrganization.Id);
            result.Message.Should().Be(ContractsMessages.Update_Success);

            var updatedOwner = await _fixture.GetOwnerAsync(expectedOwner.Id);
            var updatedOrganization = updatedOwner.GetOrganizationById(expectedOrganization.Id);
            updatedOrganization.Name.Value.Should().Be(request.Name);
        }


        #endregion
    }
}
