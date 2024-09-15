﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoMask.BuildingBlocks.Test.TestBase;
using TaskoMask.Services.Owners.Read.Api.Infrastructure.DbContext;
using TaskoMask.Services.Owners.Read.Api.Infrastructure.DI;

namespace TaskoMask.Services.Owners.Read.IntegrationTests.Fixtures;

public abstract class TestsBaseFixture : IntegrationTestsBase
{
    public readonly IMapper _mapper;
    public readonly OwnerReadDbContext _dbContext;

    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix)
    {
        _mapper = GetRequiredService<IMapper>();
        _dbContext = GetRequiredService<OwnerReadDbContext>();
    }

    /// <summary>
    ///
    /// </summary>
    public override void InitialDatabase()
    {
        ServiceProvider.InitialDatabase();
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
            //Copy from Owners.Read.Api project during the build event
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile("appsettings.Staging.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("MongoDB:DatabaseName", $"Owners_Read_DB_{dbNameSuffix}") })
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
