using System.Net.Http.Json;
using System.Xml;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using wms_android.shared.Models;
using wms_android.shared.Models.Auth;

namespace wms_android.shared.Services;

public class ApiClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;
    private string _authToken;

    public ApiClient(IConfiguration configuration)
    {
        try
        {
            // Log all available configuration values for debugging
            foreach (var setting in configuration.AsEnumerable())
            {
                System.Diagnostics.Debug.WriteLine($"Config: {setting.Key} = {setting.Value}");
            }

            _baseUrl = configuration["ApiSettings:BaseUrl"] 
                ?? throw new InvalidOperationException("API base URL not configured");
            
            System.Diagnostics.Debug.WriteLine($"Using API base URL: {_baseUrl}");
            
            _client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            
            // Test the connection
            System.Diagnostics.Debug.WriteLine("Testing API connection...");
            var healthEndpoint = $"{_baseUrl.TrimEnd('/')}/health";
            System.Diagnostics.Debug.WriteLine($"Health check URL: {healthEndpoint}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error initializing ApiClient: {ex}");
            throw;
        }
    }

    public void SetAuthToken(string token)
    {
        _authToken = token;
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    // Parcels
    public async Task<List<Parcel>> GetParcelsAsync()
    {
        var response = await _client.GetAsync("/api/parcels");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Parcel>>();
    }

    public async Task<Parcel> GetParcelAsync(Guid id)
    {
        var response = await _client.GetAsync($"/api/parcels/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Parcel>() ?? throw new Exception("Parcel not found");
    }

    public async Task<Parcel> CreateParcelAsync(Parcel parcel)
    {
        try
        {
            // Log the request payload
            System.Diagnostics.Debug.WriteLine($"Sending parcel to API: {JsonConvert.SerializeObject(parcel, Newtonsoft.Json.Formatting.Indented)}");

            var response = await _client.PostAsJsonAsync("/api/parcels", parcel);
            
            // Always read the response content
            var responseContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"API Response: Status={response.StatusCode}, Content={responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API returned {response.StatusCode}: {responseContent}");
            }

            var createdParcel = await response.Content.ReadFromJsonAsync<Parcel>();
            if (createdParcel == null)
            {
                throw new Exception("Failed to deserialize response to Parcel");
            }

            System.Diagnostics.Debug.WriteLine($"Successfully created parcel: {JsonConvert.SerializeObject(createdParcel, Newtonsoft.Json.Formatting.Indented)}");
            return createdParcel;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in CreateParcelAsync: {ex}");
            throw;
        }
    }

    // Users
    public async Task<bool> AuthenticateAsync(string username, string password)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("/api/auth/login", new { username, password });
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (token != null)
                {
                    SetAuthToken(token.Token);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AuthenticateAsync: {ex}");
            return false;
        }
    }

    // Vehicles
    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        var response = await _client.GetAsync("/api/vehicles");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Vehicle>>() ?? new List<Vehicle>();
    }

    // Error handling
    private async Task HandleErrorResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API Error: {error}");
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        try 
        {
            var fullUrl = $"{_baseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
            System.Diagnostics.Debug.WriteLine($"Making GET request to: {fullUrl}");
            
            var response = await _client.GetAsync(endpoint);
            System.Diagnostics.Debug.WriteLine($"Response from {fullUrl}: Status={response.StatusCode}");
            
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Error response content: {content}");
            }
            
            return response;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception in GetAsync for URL {_baseUrl}/{endpoint}: {ex}");
            throw;
        }
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            // Remove any quotes
            content = content.Trim('"');
            return (T)Convert.ChangeType(content, typeof(T));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"API request failed: {ex}");
            throw;
        }
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string endpoint, T data)
    {
        return await _client.PostAsJsonAsync(endpoint, data);
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
    {
        return await _client.PostAsync(endpoint, content);
    }

    public async Task<T> PostAsJsonAsync<T>(string endpoint, object data)
    {
        var response = await _client.PostAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T> PutAsJsonAsync<T>(string endpoint, object data)
    {
        var response = await _client.PutAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task DeleteAsync(string endpoint)
    {
        var response = await _client.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }
} 