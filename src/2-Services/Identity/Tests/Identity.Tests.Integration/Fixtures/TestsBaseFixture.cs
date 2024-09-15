﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoMask.BuildingBlocks.Test.TestBase;
using TaskoMask.Services.Identity.Api.Infrastructure.CrossCutting.DI;
using TaskoMask.Services.Identity.Api.Infrastructure.Data.DbContext;

namespace TaskoMask.Services.Identity.Tests.Integration.Fixtures;

public abstract class TestsBaseFixture : IntegrationTestsBase
{
    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix) { }

    /// <summary>
    ///
    /// </summary>
    public override void InitialDatabase()
    {
        ServiceProvider.InitialDatabase();
        ServiceProvider.SeedEssentialData();
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
    public override IServiceProvider GetServiceProvider(string dbNameSuffix)
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            //Copy from Identity.Api project during the build event
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile("appsettings.Staging.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("SQL:DatabaseName", $"IdentityDB_Test_{dbNameSuffix}") })
            .Build();

        services.AddSingleton<IConfiguration>(provider =>
        {
            return configuration;
        });

        services.AddLogging();

        services.AddModules(configuration);

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
