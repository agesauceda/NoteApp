using System.Net.Http.Json;
namespace NoteApp.Services.Auth
{
    public class AuthService : AuthServiceInterface
    {
        private readonly HttpClient _client;
        public AuthService(HttpClient client) {
            this._client = client;
        }
        public async Task<Models.Auth.Auth> Login(string username, string password)
        {
            return await _client.GetFromJsonAsync<Models.Auth.Auth>($"usuarios/access/?user={username}&passwd={password}");
        }
    }
}
