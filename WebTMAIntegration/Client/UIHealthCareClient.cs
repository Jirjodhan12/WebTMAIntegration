using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using WebTMAIntegration.Models;
using WebTMAIntegration.Services.Interfaces;

namespace WebTMAIntegration.Client
{
    public class UIHealthCareClient
    {
        
        private readonly HttpClient _http;
        private readonly UIHealthCareSettings _settings;
        private readonly ITokenService _tokenService;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public UIHealthCareClient(HttpClient http, IOptions<UIHealthCareSettings> settings, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _settings = settings.Value;
            _http = http;
            _http.BaseAddress = new Uri(_settings.BaseUrl + "/");
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Generic GET helper — returns deserialized T or throws
        public async Task<T> GetAsync<T>(string relativeUrl)
        {
            var token = await _tokenService.GetTokenAsync();

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(stream, JsonOptions);
            return result!;
        }
    }
}
