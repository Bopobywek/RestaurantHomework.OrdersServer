﻿using System.Transactions;
using Npgsql;
using RestaurantHomework.OrdersServer.Dal.Options;
using RestaurantHomework.OrdersServer.Dal.Repositories.Interfaces;

namespace RestaurantHomework.OrdersServer.Dal.Repositories;

public abstract class BaseRepository : IDbRepository
{
    private readonly DalOptions _dalSettings;

    protected BaseRepository(DalOptions dalSettings)
    {
        _dalSettings = dalSettings;
    }
    
    protected async Task<NpgsqlConnection> GetAndOpenConnection()
    {
        var connection = new NpgsqlConnection(_dalSettings.ConnectionString);
        await connection.OpenAsync();
        connection.ReloadTypes();
        return connection;
    }
    
    public TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions 
            { 
                IsolationLevel = level, 
                Timeout = TimeSpan.FromSeconds(5) 
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}