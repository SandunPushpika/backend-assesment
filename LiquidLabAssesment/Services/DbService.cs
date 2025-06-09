using LiquidLabAssesment.Core.Helpers;
using LiquidLabAssesment.Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LiquidLabAssesment.Services;

public class DbService : IDbService
{
    private readonly AppConfig _appConfig;

    public DbService(IOptions<AppConfig> options)
    {
        _appConfig = options.Value;
    }

    public async Task<int> ExecuteAsync(string query, params object[]? parameters)
    {
        var command = await GetCommandAsync(query, parameters);

        var rowsAffected = command.ExecuteNonQuery();
        
        await command.Connection.DisposeAsync();
        return rowsAffected;
    }

    public async Task<List<T>> FetchAllAsync<T>(string query, params object[]? parameters) where T : class, new()
    {
        await using var command = await GetCommandAsync(query, parameters);
        await using var reader = await command.ExecuteReaderAsync();
        
        var properties = typeof(T).GetProperties();
        
        List<T> list = new();
        while (await reader.ReadAsync())
        {
            var obj = new T();

            foreach (var property in properties)
            {
                var value = reader[property.Name];
                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
            }
            
            list.Add(obj);
        }

        await command.Connection.DisposeAsync();
        return list;
    }
    
    public async Task<T> FetchFirst<T>(string query, params object[]? parameters) where T : class, new()
    {
        await using var command = await GetCommandAsync(query, parameters);
        await using var reader = await command.ExecuteReaderAsync();
        
        var properties = typeof(T).GetProperties();

        if (await reader.ReadAsync())
        {
            var obj = new T();
            foreach (var property in properties)
            {
                var value = reader[property.Name];
                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
            }
            
            return obj;
        }
        
        await command.Connection.DisposeAsync();
        return null;
    }

    private async Task<SqlCommand> GetCommandAsync(string query, params object[]? parameters)
    {
        var con = new SqlConnection(_appConfig.ConnectionString);

        var command = new SqlCommand(query, con);
        if (!parameters.IsNullOrEmpty())
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                command.Parameters.AddWithValue($"@p{i}", parameters[i] ?? DBNull.Value);
            }
        }
        await con.OpenAsync();
        return command;
    }
    
}