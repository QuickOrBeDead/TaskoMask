﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TaskoMask.BuildingBlocks.Infrastructure.MongoDB;

/// <summary>
///
/// </summary>
public abstract class MongoDbContext
{
    #region Fields

    private readonly MongoDbOptions _mongoDbOptions;
    private readonly IMongoDatabase _database;
    private readonly IMongoClient _client;

    #endregion

    #region Ctors


    public MongoDbContext(IOptions<MongoDbOptions> mongoDbOptions)
    {
        _mongoDbOptions = mongoDbOptions.Value;
        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_mongoDbOptions.Connection));
        _client = new MongoClient(settings);
        _database = _client.GetDatabase(_mongoDbOptions.DatabaseName);
    }

    #endregion

    #region Public Methods



    /// <summary>
    ///
    /// </summary>
    public IMongoCollection<TEntity> GetCollection<TEntity>(string name = "")
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(TEntity).Name + "s";

        return _database.GetCollection<TEntity>(name);
    }

    /// <summary>
    ///
    /// </summary>
    public void DropDatabase()
    {
        _client.DropDatabase(_mongoDbOptions.DatabaseName);
    }

    #endregion
}
