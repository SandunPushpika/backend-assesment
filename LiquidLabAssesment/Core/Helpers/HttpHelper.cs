using System.Net;
using System.Text.Json;

namespace LiquidLabAssesment.Core.Helpers;

public class HttpHelper
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpHelper> _logger;
    
    public HttpHelper(HttpClient httpClient, ILogger<HttpHelper> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var responseMessage = await _httpClient.GetAsync(url);
        if (!responseMessage.IsSuccessStatusCode)
        {
            _logger.LogError($"Error fetching: {url}");
            _logger.LogError(responseMessage.Content.ReadAsStringAsync().Result);
            
            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Resource not found");
            }
            
            throw new Exception("Error retreiving data from API");
        }
        
        var responseData = await responseMessage.Content.ReadAsStringAsync();
        var convertedValue = JsonSerializer.Deserialize<T>(responseData, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });
        return convertedValue;
    }
    
}