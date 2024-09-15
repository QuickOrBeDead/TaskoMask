﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Test.TestBase;
using TaskoMask.Services.Boards.Write.Api.Domain.Boards.Data;
using TaskoMask.Services.Boards.Write.Api.Domain.Boards.Entities;
using TaskoMask.Services.Boards.Write.Api.Domain.Boards.Services;
using TaskoMask.Services.Boards.Write.Api.Infrastructure.CrossCutting.DI;
using TaskoMask.Services.Boards.Write.Api.Infrastructure.Data.DbContext;

namespace TaskoMask.Services.Boards.Write.Tests.Integration.Fixtures;

public abstract class TestsBaseFixture : IntegrationTestsBase
{
    public readonly IBoardAggregateRepository _boardAggregateRepository;
    public readonly IBoardValidatorService _boardValidatorService;
    public readonly IMessageBus _messageBus;
    public readonly IInMemoryBus _inMemoryBus;

    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix)
    {
        _boardAggregateRepository = GetRequiredService<IBoardAggregateRepository>();
        _boardValidatorService = GetRequiredService<IBoardValidatorService>();
        _messageBus = Substitute.For<IMessageBus>();
        _inMemoryBus = Substitute.For<IInMemoryBus>();
    }

    /// <summary>
    ///
    /// </summary>
    public override void InitialDatabase()
    {
        ServiceProvider.InitialDatabasesAndSeedEssentialData();
    }

    /// <summary>
    ///
    /// </summary>
    public override void DropDatabase()
    {
        ServiceProvider.DropDatabase();
    }

    /// <summary>
    ///
    /// </summary>
    public async Task SeedBoardAsync(Board board)
    {
        await _boardAggregateRepository.AddAsync(board);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Board> GetBoardAsync(string id)
    {
        return await _boardAggregateRepository.GetByIdAsync(id);
    }

    /// <summary>
    ///
    /// </summary>
    public override IServiceProvider GetServiceProvider(string dbNameSuffix)
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            //Copy from Boards.Write.Api card during the build event
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile("appsettings.Staging.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("MongoDB:DatabaseName", $"Boards_Write_DB_{dbNameSuffix}") })
            .Build();

        services.AddSingleton<IConfiguration>(provider =>
        {
            return configuration;
        });

        services.AddModules(configuration);

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
