using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using WebTMAIntegration.Models;
using WebTMAIntegration.Services.Interfaces;
using static System.Net.WebRequestMethods;

namespace WebTMAIntegration.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly UIHealthCareSettings _settings;

        private string? _token;
        private DateTime _expiresAt;

        public TokenService( HttpClient httpClient, IOptions<UIHealthCareSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl + "/");
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetTokenAsync()
        {
            // Generate Access Token 5 minutes earlier before expired the token 
            if (!string.IsNullOrEmpty(_token) && _expiresAt > DateTime.UtcNow.AddMinutes(5))
            {
                return _token;
            }

            await GenerateTokenAsync();

            return _token!;
        }

        private async Task GenerateTokenAsync()
        {
            var request = new
            {
                UserName = _settings.UserName,
                Password = _settings.Password,
                ClientName = _settings.ClientName,
            };

            var response = await _httpClient.PostAsJsonAsync(
                "Users/Authenticate",
                request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            _token = result.Token;
            _expiresAt = result.ExpiredTime;

        }

    }
}
