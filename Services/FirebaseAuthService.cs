using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace StudentManagementSystem.Services
{
    public class FirebaseAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FirebaseAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<FirebaseAuthResult> RegisterAsync(string email, string password)
        {
            var apiKey = GetApiKey();
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, new
            {
                email,
                password,
                returnSecureToken = true
            });

            return await ParseResponse(response);
        }

        public async Task<FirebaseAuthResult> LoginAsync(string email, string password)
        {
            var apiKey = GetApiKey();
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, new
            {
                email,
                password,
                returnSecureToken = true
            });

            return await ParseResponse(response);
        }

        private string GetApiKey()
        {
            var apiKey = _configuration["Firebase:WebApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("Firebase:WebApiKey is missing from appsettings.json.");

            return apiKey;
        }

        private static async Task<FirebaseAuthResult> ParseResponse(HttpResponseMessage response)
        {
            var body = await response.Content.ReadFromJsonAsync<FirebaseResponse>();

            if (!response.IsSuccessStatusCode)
            {
                var message = body?.Error?.Message ?? "Firebase Authentication failed.";
                throw new FirebaseAuthServiceException(message);
            }

            if (body == null || string.IsNullOrWhiteSpace(body.LocalId) || string.IsNullOrWhiteSpace(body.IdToken))
                throw new FirebaseAuthServiceException("Firebase returned an invalid authentication response.");

            return new FirebaseAuthResult(body.LocalId, body.Email ?? string.Empty, body.IdToken);
        }

        private sealed class FirebaseResponse
        {
            [JsonPropertyName("localId")]
            public string? LocalId { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [JsonPropertyName("idToken")]
            public string? IdToken { get; set; }

            [JsonPropertyName("error")]
            public FirebaseError? Error { get; set; }
        }

        private sealed class FirebaseError
        {
            [JsonPropertyName("message")]
            public string? Message { get; set; }
        }
    }

    public record FirebaseAuthResult(string UserId, string Email, string IdToken);

    public class FirebaseAuthServiceException : Exception
    {
        public FirebaseAuthServiceException(string message) : base(message) { }
    }
}
