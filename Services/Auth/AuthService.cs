using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
