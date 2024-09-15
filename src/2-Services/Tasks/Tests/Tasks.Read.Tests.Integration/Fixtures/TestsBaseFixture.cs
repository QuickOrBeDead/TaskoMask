﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TaskoMask.BuildingBlocks.Test.TestBase;
using TaskoMask.Services.Tasks.Read.Api.Infrastructure.DbContext;
using TaskoMask.Services.Tasks.Read.Api.Infrastructure.DI;

namespace TaskoMask.Services.Tasks.Read.Tests.Integration.Fixtures;

public abstract class TestsBaseFixture : IntegrationTestsBase
{
    public readonly IMapper _mapper;
    public readonly TaskReadDbContext _dbContext;

    protected TestsBaseFixture(string dbNameSuffix)
        : base(dbNameSuffix)
    {
        _mapper = GetRequiredService<IMapper>();
        _dbContext = GetRequiredService<TaskReadDbContext>();
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
            //Copy from Tasks.Read.Api card during the build event
            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
            .AddJsonFile("appsettings.Staging.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("MongoDB:DatabaseName", $"Tasks_Read_DB_{dbNameSuffix}") })
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
