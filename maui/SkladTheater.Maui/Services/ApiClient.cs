using Newtonsoft.Json;
using SkladTheater.Maui.Helpers;
using System.Net.Http.Headers;
using System.Text;

namespace SkladTheater.Maui.Services;

public interface IApiClient
{
    Task<T?> GetAsync<T>(string url, CancellationToken ct = default);
    Task<T?> PostAsync<T>(string url, object? body = null, CancellationToken ct = default);
    Task<T?> PutAsync<T>(string url, object? body = null, CancellationToken ct = default);
    Task<T?> PatchAsync<T>(string url, object? body = null, CancellationToken ct = default);
    Task<T?> DeleteAsync<T>(string url, CancellationToken ct = default);
    Task<T?> PostMultipartAsync<T>(string url, StreamContent content, string name, string fileName, CancellationToken ct = default);
}

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    public ApiClient(ITokenService tokenService)
    {
        _tokenService = tokenService;
        var handler = HttpClientHandlerProvider.Create();
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(AppConfig.BaseUrl),
            Timeout = TimeSpan.FromSeconds(AppConfig.ReadTimeoutSeconds)
        };
    }

    private async Task ApplyAuthHeaderAsync(CancellationToken ct)
    {
        var token = await _tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public async Task<T?> GetAsync<T>(string url, CancellationToken ct = default)
    {
        await ApplyAuthHeaderAsync(ct);
        var response = await _httpClient.GetAsync(url, ct);
        await EnsureSuccessOrThrowAsync(response);
        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T?> PostAsync<T>(string url, object? body = null, CancellationToken ct = default)
    {
        await ApplyAuthHeaderAsync(ct);
        var content = CreateJsonContent(body);
        var response = await _httpClient.PostAsync(url, content, ct);
        await EnsureSuccessOrThrowAsync(response);
        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T?> PutAsync<T>(string url, object? body = null, CancellationToken ct = default)
    {
        await ApplyAuthHeaderAsync(ct);
        var content = CreateJsonContent(body);
        var response = await _httpClient.PutAsync(url, content, ct);
        await EnsureSuccessOrThrowAsync(response);
        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T?> PatchAsync<T>(string url, object? body = null, CancellationToken ct = default)
    {
        await ApplyAuthHeaderAsync(ct);
        var content = CreateJsonContent(body);
        var response = await _httpClient.PatchAsync(url, content, ct);
        await EnsureSuccessOrThrowAsync(response);
        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T?> DeleteAsync<T>(string url, CancellationToken ct = default)
    {
        await ApplyAuthHeaderAsync(ct);
        var response = await _httpClient.DeleteAsync(url, ct);
        await EnsureSuccessOrThrowAsync(response);
        var json = await response.Content.ReadAsStringAsync(ct);
        return string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T?> PostMultipartAsync<T>(string url, StreamContent content, string name, string fileName, CancellationToken ct = default)
    {
        await ApplyAuthHeaderAsync(ct);
        var multipart = new MultipartFormDataContent();
        multipart.Add(content, name, fileName);
        var response = await _httpClient.PostAsync(url, multipart, ct);
        await EnsureSuccessOrThrowAsync(response);
        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonConvert.DeserializeObject<T>(json);
    }

    private static HttpContent CreateJsonContent(object? body)
    {
        if (body == null) return new StringContent("{}", Encoding.UTF8, "application/json");
        var json = JsonConvert.SerializeObject(body);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private static async Task EnsureSuccessOrThrowAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;
        var content = await response.Content.ReadAsStringAsync();
        throw new ApiException((int)response.StatusCode, content, response.ReasonPhrase);
    }
}

public class ApiException : Exception
{
    public int StatusCode { get; }
    public string ResponseContent { get; }

    public ApiException(int statusCode, string responseContent, string? reasonPhrase)
        : base($"API error {(int)statusCode}: {reasonPhrase}")
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }
}
