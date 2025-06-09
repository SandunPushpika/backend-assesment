using Microsoft.Data.SqlClient;

namespace LiquidLabAssesment.Core.Interfaces;

public interface IDbService
{
    Task<int> ExecuteAsync(string query, params object[]? parameters);
    Task<List<T>> FetchAllAsync<T>(string query, params object[]? parameters) where T : class, new();
    Task<T> FetchFirst<T>(string query, params object[]? parameters) where T : class, new();
}